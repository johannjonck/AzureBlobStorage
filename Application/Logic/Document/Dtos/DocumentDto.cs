namespace Application.Dtos
{
    public class DocumentDto
    {
        public string FileName { get; set; }

        public string FileFullName { get; set; }

        public long FileSize { get; set; }

        public DateTime DateModified { get; set; }

    }
}
