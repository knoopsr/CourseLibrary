namespace CourseLibrary.API.Models
{
    public class AuthorDto
    {
        private Guid _guid;
        public Guid Id
        {
            get
            {
                return _guid;
            }
            set
            {
                _guid = value;
            }
        }

        private string _name;
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
            }
        }

        private int _age;
        public int Age
        {
            get
            {
                return _age;
            }
            set
            {
                _age = value;
            }
        }

        private string _mainCategory;
        public string MainCategory
        {
            get
            {
                return _mainCategory;
            }
            set
            {
                _mainCategory = value;
            }
        }
    }
}
