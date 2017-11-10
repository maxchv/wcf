using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.ServiceModel;
using System.Timers;

namespace TestingSystemService
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single,
        ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class TestingSystemService : ITestingSystemService
    {
        private List<IServiceCallback> _clientsCallback = new List<IServiceCallback>();
        private ConcurrentDictionary<string, TestOnline> _onlineTests = new ConcurrentDictionary<string, TestOnline>();
        private object _locker = new object();

        private IServiceCallback Callback
        {
            get
            {
                IServiceCallback callback = OperationContext.Current?.GetCallbackChannel<IServiceCallback>();
                return callback;
            }
        }

        public void NewClientConnected()
        {
            try
            {
                IServiceCallback callback = Callback;
                if (callback != null)
                {
                    _clientsCallback.Add(callback);
                }
            }
            catch (Exception)
            {
                // ignored
            }
        }

        public void ClientDisconnected()
        {
            try
            {
                IServiceCallback callback = Callback;
                if (callback != null)
                {
                    _clientsCallback.Remove(callback);
                }
            }
            catch (Exception)
            {
                // ignored
            }
        }

        public bool RegisterNewUser(User user)
        {
            bool isRegister;

            try
            {
                using (TestingSystemContext context = new TestingSystemContext())
                {
                    context.Users.Add(user);
                    context.SaveChanges();
                }

                isRegister = true;
            }
            catch (Exception)
            {
                isRegister = false;
            }

            return isRegister;
        }

        public void SignIn(string login, string password)
        {
            try
            {

                IServiceCallback callback = Callback;
                if (callback != null)
                {

                    using (TestingSystemContext context = new TestingSystemContext())
                    {
                        User user = context.Users.FirstOrDefault(u => u.Login.Equals(login));

                        if (user is null)
                        {
                            callback.ResultOfUserSignIn(null, "Пользователь с таким логином не найден!");
                        }
                        else
                        {
                            if (user.Password.Equals(password))
                            {
                                callback.ResultOfUserSignIn(user, "");
                            }
                            else
                            {
                                callback.ResultOfUserSignIn(null, "Неверный пароль!");
                            }
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                IServiceCallback callback = Callback;
                if (callback != null)
                {
                    callback.ResultOfUserSignIn(null, exception.Message);
                }
            }
        }

        public void CreateNewTest(Test test)
        {
            try
            {
                using (TestingSystemContext context = new TestingSystemContext())
                {
                    using (var transaction = context.Database.BeginTransaction())
                    {
                        try
                        {
                            foreach (Question question in test.Questions)
                            {
                                context.Questions.Add(question);
                                context.SaveChanges();
                            }

                            context.Tests.Add(test);
                            context.SaveChanges();

                            transaction.Commit();
                        }
                        catch (Exception)
                        {
                            transaction.Rollback();
                        }
                    }
                }

                Dictionary<string, string> tests = SendTestsListToClients();
                foreach (IServiceCallback callback in _clientsCallback)
                {
                    callback.SetTestList(tests);
                }
            }
            catch (Exception)
            {
                // ignored
            }
        }

        public void GetListOfTests()
        {
            IServiceCallback callback = Callback;
            if (callback != null)
            {
                try
                {
                    Dictionary<string, string> tests = SendTestsListToClients();
                    Callback.SetTestList(tests);
                }
                catch (Exception)
                {
                    // ignored
                }
            }
        }

        private Dictionary<string, string> SendTestsListToClients()
        {
            Dictionary<string, string> tests = new Dictionary<string, string>();

            try
            {
                using (TestingSystemContext context = new TestingSystemContext())
                {
                    foreach (var test in context.Tests)
                    {
                        tests.Add(test.Id, test.Name);
                    }
                }
            }
            catch (Exception)
            {
                // ignored
            }

            return tests;
        }

        private void TimerExOnElapsed(object sender, ElapsedEventArgs elapsedEventArgs)
        {
            try
            {
                TimerEx timer = sender as TimerEx;

                if (timer != null)
                {
                    if (timer.TimeLeft-- == 0)
                    {
                        timer.Stop();
                        timer.Callback.ForciblyReturnTestResult();
                    }
                    else
                    {
                        timer.Callback.SetRemainingTime(timer.TimeLeft);
                    }
                }
            }
            catch (Exception)
            {
                // ignored
            }
        }

        private TimerEx GetTimer(long timeLeft, IServiceCallback callback)
        {
            TimerEx timerEx = new TimerEx();
            timerEx.Interval = 1000;
            timerEx.Elapsed += TimerExOnElapsed;
            timerEx.Id = Guid.NewGuid().ToString();
            timerEx.TimeLeft = timeLeft;
            timerEx.Callback = callback;

            return timerEx;
        }

        public void StartTesting(User user, string testId)
        {
            lock (_locker)
            {
                try
                {
                    bool sendData = false;
                    Test test;
                    using (TestingSystemContext context = new TestingSystemContext())
                    {
                        test = context.Tests.Include(c => c.Questions).FirstOrDefault(t => t.Id.Equals(testId));
                    }

                    if (test != null)
                    {
                        TestOnline testOnline;
                        bool isFoundTest = _onlineTests.TryGetValue(testId, out testOnline);

                        // если тест уже есть в списке
                        if (isFoundTest)
                        {
                            // если есть группы
                            if (testOnline.Groups.Count > 0)
                            {
                                // проходим циклом по всем группам
                                for (int i = 0; i < testOnline.Groups.Count; i++)
                                {
                                    if (sendData)
                                        break;

                                    // если кол-во подгрупп в группе равно макс. кол-ву подгрупп в группе 
                                    // значит свободных мест нет и надо идти дальше
                                    if (testOnline.Groups[i].Subgroups.Count == testOnline.MaxCountInGroup)
                                    {
                                        // если i+1 меньше кол-ва групп
                                        // значит есть еще подгруппа
                                        if (i + 1 < testOnline.Groups.Count)
                                        {
                                            continue;
                                        }
                                        else
                                        {
                                            // иначе больше групп нет надо создавать новую группу
                                            // и вытащить все вопросы с предыдущей группы и найти не повторяющиеся вопросы
                                            // найти первый не повтор и добавить его в список
                                            // после добавлен проверить если кол-во вопросов в списке равно
                                            // кол-ву вопросов всего в тесте то начать сначала

                                            IServiceCallback callback = Callback;

                                            if (callback != null)
                                            {
                                                // список вопросов которые есть в предыдущей группе
                                                List<Question> repeatQuestions = new List<Question>();

                                                int index = i - 1 < 0 ? 0 : i - 1;
                                                // если есть предыдущая группа
                                                if (index >= 0)
                                                {
                                                    // проходим циклом по каждой подгруппе
                                                    for (int j = 0; j < testOnline.Groups[index].Subgroups.Count; j++)
                                                    {
                                                        // добавляем все вопросы из каждой подгруппы в список
                                                        for (int k = 0;
                                                            k < testOnline.Groups[index].Subgroups[j].Questions.Count;
                                                            k++)
                                                        {
                                                            repeatQuestions.Add(testOnline.Groups[index]
                                                                .Subgroups[j]
                                                                .Questions[k]);
                                                        }
                                                    }
                                                }

                                                Group newGroup = new Group();
                                                newGroup.Id = Guid.NewGuid().ToString();

                                                Subgroup newSubgroup = new Subgroup();
                                                newSubgroup.Id = Guid.NewGuid().ToString();
                                                newSubgroup.Callback = callback;
                                                newSubgroup.Timer =
                                                    GetTimer(test.NumberMinutesToPassTest * 60, callback);

                                                List<Question> questions = new List<Question>();
                                                for (int j = 0; j < test.NumberOfQuestions; j++)
                                                {
                                                    for (int k = 0; k < test.Questions.Count; k++)
                                                    {
                                                        // поиск вопроса с указанным ид в списке выданных вопросов
                                                        Question question =
                                                            repeatQuestions.FirstOrDefault(q => q.Id.Equals(test
                                                                .Questions[k]
                                                                .Id));

                                                        // если не найдено значит вопрос еще не выдавался
                                                        if (question == null)
                                                        {
                                                            questions.Add(test.Questions[k]);
                                                            repeatQuestions.Add(test.Questions[k]);

                                                            // если кол-во вопр в списке равно общему кол-во вопросов в тесте
                                                            // то очищаем список
                                                            if (repeatQuestions.Count == test.Questions.Count)
                                                            {
                                                                repeatQuestions.Clear();
                                                            }
                                                            break;
                                                        }
                                                    }
                                                }

                                                newSubgroup.Questions = questions;

                                                List<Subgroup> subgroups = new List<Subgroup>();
                                                subgroups.Add(newSubgroup);

                                                newGroup.Subgroups = subgroups;

                                                testOnline.Groups.Add(newGroup);

                                                bool isUpdate = _onlineTests.TryUpdate(testId, testOnline, testOnline);

                                                if (isUpdate)
                                                {
                                                    sendData = true;
                                                    callback.SetQuestions(questions, newGroup.Id, newSubgroup.Id);
                                                    newSubgroup.Timer.Start();
                                                }
                                            }
                                        }
                                    }
                                    // иначе в группе есть свободное место для новой погруппы
                                    else
                                    {
                                        // найти свободные вопросы
                                        // взять все вопросы текущей группы и найти недостающие

                                        IServiceCallback callback = Callback;

                                        if (callback != null)
                                        {
                                            // список вопросов которые есть в текущей группе
                                            List<Question> repeatQuestions = new List<Question>();

                                            // проходим циклом по каждой подгруппе
                                            for (int j = 0; j < testOnline.Groups[i].Subgroups.Count; j++)
                                            {
                                                // добавляем все вопросы из каждой подгруппы в список
                                                for (int k = 0;
                                                    k < testOnline.Groups[i].Subgroups[j].Questions.Count;
                                                    k++)
                                                {
                                                    repeatQuestions.Add(testOnline.Groups[i].Subgroups[j].Questions[k]);
                                                }
                                            }

                                            Subgroup newSubgroup = new Subgroup();
                                            newSubgroup.Id = Guid.NewGuid().ToString();
                                            newSubgroup.Callback = callback;
                                            newSubgroup.Timer = GetTimer(test.NumberMinutesToPassTest * 60, callback);

                                            List<Question> questions = new List<Question>();

                                            for (int j = 0; j < test.NumberOfQuestions; j++)
                                            {
                                                for (int k = 0; k < test.Questions.Count; k++)
                                                {
                                                    // поиск вопроса с указанным ид в списке выданных вопросов
                                                    Question question =
                                                        repeatQuestions.FirstOrDefault(
                                                            q => q.Id.Equals(test.Questions[k].Id));

                                                    // если не найдено значит вопрос еще не выдавался
                                                    if (question == null)
                                                    {
                                                        questions.Add(test.Questions[k]);
                                                        repeatQuestions.Add(test.Questions[k]);

                                                        // если кол-во вопр в списке равно общему кол-во вопросов в тесте
                                                        // то очищаем список
                                                        if (repeatQuestions.Count == test.Questions.Count)
                                                        {
                                                            repeatQuestions.Clear();
                                                        }
                                                        break;
                                                    }
                                                }
                                            }

                                            newSubgroup.Questions = questions;
                                            testOnline.Groups[i].Subgroups.Add(newSubgroup);

                                            bool isUpdate = _onlineTests.TryUpdate(testId, testOnline, testOnline);

                                            if (isUpdate)
                                            {
                                                sendData = true;
                                                callback.SetQuestions(questions, testOnline.Groups[i].Id,
                                                    newSubgroup.Id);
                                                newSubgroup.Timer.Start();
                                            }
                                        }
                                    }
                                }
                            }
                            else
                            {
                                // если групп нет то создаем новую (первую) группу

                                IServiceCallback callback = Callback;

                                if (callback != null)
                                {
                                    Group newGroup = new Group();
                                    newGroup.Id = Guid.NewGuid().ToString();

                                    Subgroup newSubgroup = new Subgroup();
                                    newSubgroup.Id = Guid.NewGuid().ToString();
                                    newSubgroup.Callback = callback;
                                    newSubgroup.Timer = GetTimer(test.NumberMinutesToPassTest * 60, callback);

                                    List<Question> questions = new List<Question>();
                                    for (int i = 0; i < test.NumberOfQuestions; i++)
                                    {
                                        questions.Add(test.Questions[i]);
                                    }

                                    newSubgroup.Questions = questions;

                                    List<Subgroup> subgroups = new List<Subgroup>();
                                    subgroups.Add(newSubgroup);

                                    newGroup.Subgroups = subgroups;

                                    List<Group> groups = new List<Group>();
                                    groups.Add(newGroup);

                                    testOnline.Groups = groups;

                                    sendData = true;
                                    callback.SetQuestions(questions, newGroup.Id, newSubgroup.Id);
                                    newSubgroup.Timer.Start();
                                }
                            }
                        }
                        else
                        {
                            IServiceCallback callback = Callback;

                            if (callback != null)
                            {
                                // если теста нет еще в списке, то добавляем его туда
                                TestOnline newTestOnline = new TestOnline();

                                // находим максимальное количество подгрупп в одной группе 
                                // кол-во вопросов / кол-во выдаваемых вопросов
                                newTestOnline.MaxCountInGroup = test.Questions.Count / test.NumberOfQuestions;

                                Group newGroup = new Group();
                                newGroup.Id = Guid.NewGuid().ToString();

                                Subgroup newSubgroup = new Subgroup();
                                newSubgroup.Id = Guid.NewGuid().ToString();
                                newSubgroup.Callback = callback;
                                newSubgroup.Timer = GetTimer(test.NumberMinutesToPassTest * 60, callback);

                                List<Question> questions = new List<Question>();
                                for (int i = 0; i < test.NumberOfQuestions; i++)
                                {
                                    questions.Add(test.Questions[i]);
                                }

                                newSubgroup.Questions = questions;

                                List<Subgroup> subgroups = new List<Subgroup>();
                                subgroups.Add(newSubgroup);

                                newGroup.Subgroups = subgroups;

                                List<Group> groups = new List<Group>();
                                groups.Add(newGroup);

                                newTestOnline.Groups = groups;

                                _onlineTests.TryAdd(test.Id, newTestOnline);

                                callback.SetQuestions(questions, newGroup.Id, newSubgroup.Id);
                                newSubgroup.Timer.Start();
                            }
                        }
                    }
                }
                catch (Exception)
                {
                    // ignored
                }
            }
        }

        public void EndTesting(TestResult result, string groupId, string subgroupId, string testId)
        {
            IServiceCallback callback = Callback;

            if (callback != null)
            {
                try
                {
                    TestOnline testOnline;
                    bool isFoundTest = _onlineTests.TryGetValue(testId, out testOnline);

                    if (isFoundTest)
                    {
                        if (testOnline.Groups.Count > 0)
                        {
                            Group group = testOnline.Groups.FirstOrDefault(g => g.Id.Equals(groupId));
                            Subgroup subgroup = group?.Subgroups.FirstOrDefault(s => s.Id.Equals(subgroupId));

                            if (subgroup != null)
                            {
                                try
                                {
                                    subgroup.Timer.Stop();
                                }
                                catch
                                {
                                    // ignored 
                                }

                                group.Subgroups.Remove(subgroup);

                                if (group.Subgroups.Count == 0)
                                {
                                    testOnline.Groups.Remove(group);
                                }

                                using (TestingSystemContext context = new TestingSystemContext())
                                {
                                    using (var transaction = context.Database.BeginTransaction())
                                    {
                                        try
                                        {
                                            foreach (QuestionWithAnswer question in result.Questions)
                                            {
                                                context.QuestionWithAnswers.Add(question);
                                                context.SaveChanges();
                                            }

                                            User user = context.Users.FirstOrDefault(u => u.Id.Equals(result.User.Id));

                                            if (user != null)
                                                result.User = user;

                                            Test test = context.Tests.FirstOrDefault(t => t.Id.Equals(testId));

                                            if (test != null)
                                                result.Test = test;

                                            context.Results.Add(result);
                                            context.SaveChanges();

                                            transaction.Commit();

                                            callback.ResultEndTesting(true, "Результаты теста были успешно сохранены!");
                                        }
                                        catch (Exception exception)
                                        {
                                            transaction.Rollback();

                                            callback.ResultEndTesting(false, exception.Message);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                catch (Exception exception)
                {
                    callback.ResultEndTesting(false, exception.Message);
                }
            }
        }

        public void GetResultTest(string testId)
        {
            try
            {
                using (TestingSystemContext context = new TestingSystemContext())
                {
                    TestResult[] results = context.Results.Include(r => r.Test)
                        .Include(r => r.User)
                        .Include(r => r.Questions)
                        .Where(r => r.Test.Id.Equals(testId))
                        .ToArray();

                    Callback?.SetTestResults(results);
                }
            }
            catch (Exception)
            {
                Callback?.SetTestResults(null);
            }
        }

        public bool LoginIsFree(string login)
        {
            bool isFree = false;

            try
            {
                using (TestingSystemContext context = new TestingSystemContext())
                {
                    User user = context.Users.FirstOrDefault(u => u.Login.Equals(login));

                    if (user is null)
                    {
                        isFree = true;
                    }
                }
            }
            catch (Exception)
            {
                // ignored
            }

            return isFree;
        }

        public bool ChangeRating(string resultId, int newRating)
        {
            bool isChange;
            try
            {
                using (TestingSystemContext context = new TestingSystemContext())
                {
                    TestResult result = context.Results.FirstOrDefault(r => r.Id.Equals(resultId));

                    if (result is null)
                    {
                        isChange = false;
                    }
                    else
                    {
                        result.Rating = newRating;
                        context.SaveChanges();
                        isChange = true;
                    }
                }
            }
            catch (Exception)
            {
                isChange = false;
            }

            return isChange;
        }

        public void GetResultsTest(string testId, string userId)
        {
            try
            {
                using (TestingSystemContext context = new TestingSystemContext())
                {
                    TestResult[] results = context.Results.Include(r => r.Test)
                        .Include(r => r.User)
                        .Include(r => r.Questions)
                        .Where(r => r.Test.Id.Equals(testId) && r.User.Id.Equals(userId))
                        .ToArray();

                    Callback?.SetTestResults(results);
                }
            }
            catch (Exception)
            {
                Callback?.SetTestResults(null);
            }
        }
    }
}