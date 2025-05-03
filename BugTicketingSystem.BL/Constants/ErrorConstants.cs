namespace BugTicketingSystem.BL.Constants
{
    public static class ErrorConstants
    {
        public static class ErrorMessages
        {
            public const string NameRequired = "Name Can't be empty";
            public const string EmailRequired = "Email Can't be empty";
            public const string FilePathRequired = "File Path Required";
            public const string UserRoleRequired = "User Role Required";
            public const string BugStatusRequired = "Bug Status Required";
            public const string NameCannotBeShorterThan3Characters = "Name cannot be shorter than 3 characters";
            public const string NameMustBeUnique = "Name must be unique";
            public const string EmailMustBeUnique = "Email must be unique";
            public const string FileNameMustBeUnique = "File Name must be unique";
            public const string ProjectNameMustBeUnique = "project Name must be unique";
            public const string StrongPassward = "Password cannot be shorter than 8 characters & Must have special characters";
            public const string InvalidBugStatus = "Invalid bug status value";
            public const string InvalidUserRole = "Invalid user role value";
            public const string InvalidEmailFormat = "Invalid email format";


        }
        public static class ErrorCodes
        {
            public const string NameRequired = "ERR-01";
            public const string EmailRequired = "ERR-09";
            public const string FilePathRequired = "ERR-10";
            public const string UserRoleRequired = "ERR-02";
            public const string BugStatusRequired = "ERR-03";
            public const string NameMustBeUnique = "ERR-04";
            public const string NameCannotBeShorterThan3Characters = "ERR-05";
            public const string EmailMustBeUnique = "ERR-06";
            public const string FileNameMustBeUnique = "ERR-07";
            public const string ProjectNameMustBeUnique = "ERR-08";
            public const string StrongPassward = "ERR-11";
            public const string InvalidBugStatus = "ERR-12";
            public const string InvalidUserRole = "ERR-13";
            public const string InvalidEmailFormat = "ERR-14";

        }
    }

}
