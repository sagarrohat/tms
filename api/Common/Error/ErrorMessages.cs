namespace Common;

public static class ErrorMessages
{
    #region General
    public const string InvalidCredentials = "Either your password or username are incorrect.";
    public const string Required = "{0} is required.";
    public const string Incorrect = "Incorrect value for {0}.";

    public const string Exception = "Well, this is embarrassing. Something went wrong and I don't know why!";
    #endregion

    #region Task
    public const string TaskNotFound = "No task with ID: {0}.";
    public const string TaskCompletedOrCancelled = "Task with ID: {0} is Completed or Cancelled.";
    #endregion
}

