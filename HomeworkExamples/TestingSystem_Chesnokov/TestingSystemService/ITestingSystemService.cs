using System.Collections.Generic;
using System.ServiceModel;

namespace TestingSystemService
{
    [ServiceContract(CallbackContract = typeof(IServiceCallback))]
    public interface ITestingSystemService
    {
        [OperationContract(IsOneWay = true)]
        void NewClientConnected();

        [OperationContract(IsOneWay = true)]
        void ClientDisconnected();

        [OperationContract]
        bool LoginIsFree(string login);

        [OperationContract]
        bool RegisterNewUser(User user);

        [OperationContract(IsOneWay = true)]
        void SignIn(string login, string password);

        [OperationContract(IsOneWay = true)]
        void CreateNewTest(Test test);

        [OperationContract(IsOneWay = true)]
        void GetListOfTests();

        [OperationContract(IsOneWay = true)]
        void StartTesting(User user, string testId);

        [OperationContract(IsOneWay = true)]
        void EndTesting(TestResult result, string groupId, string subgroupId, string testId);

        [OperationContract(IsOneWay = true)]
        void GetResultTest(string testId);

        [OperationContract]
        bool ChangeRating(string resultId, int newRating);

        [OperationContract(IsOneWay = true)]
        void GetResultsTest(string testId, string userId);
    }

    [ServiceContract]
    public interface IServiceCallback
    {
        [OperationContract(IsOneWay = true)]
        void SetTestList(Dictionary<string, string> testsList);

        [OperationContract(IsOneWay = true)]
        void SetQuestions(List<Question> questions, string groupId, string subgroupId);

        [OperationContract(IsOneWay = true)]
        void ForciblyReturnTestResult();

        [OperationContract(IsOneWay = true)]
        void ResultOfUserSignIn(User user, string message);

        [OperationContract(IsOneWay = true)]
        void ResultEndTesting(bool result, string message);

        [OperationContract(IsOneWay = true)]
        void SetTestResults(TestResult[] results);

        [OperationContract(IsOneWay = true)]
        void SetRemainingTime(long second);
    }
}