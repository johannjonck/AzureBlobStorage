namespace Application.Logic.Document.Validators
{
    public class DocumentValidationMessages
    {
        private static readonly string Request_Name = "AddDocumentRequest:";

        public static string File_Name = $"{Request_Name} File name must be specified.";
        public static string File_FullName = $"{Request_Name} File full name must be specified.";
        public static string File_Size = $"{Request_Name} File size must be greater than 0.";
    }
}
