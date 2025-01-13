using CourseLibrary.API.DbContexts;
using CourseLibrary.API.Entities;

namespace CourseLibrary.API.Services
{
    public class clsCourseLibraryRepository : ICourseLibraryRepository, IDisposable
    {
        private readonly CourseLibraryContext _context;

        public clsCourseLibraryRepository(CourseLibraryContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }


        public void AddAuthor(Author author)
        {
            if (author == null)
            {
                throw new ArgumentNullException(nameof(author));
            }
            author.Id = Guid.NewGuid();
            foreach (var course in author.Courses)
            {
                course.Id = Guid.NewGuid();
            }
            _context.Authors.Add(author);
        }

        public void AddCourse(Guid authorId, Course course)
        {
            if (authorId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(authorId));
            }

            if (course == null)
            {
                throw new ArgumentNullException(nameof(course));
            }

            course.AuthorId = authorId;
            _context.Courses.Add(course);
        }

        public bool AuthorExists(Guid authorId)
        {
            if (authorId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(authorId));
            }
            return _context.Authors.Any(a => a.Id == authorId);
        }

        public void DeleteAuthor(Author author)
        {
            if (author == null)
            {
                throw new ArgumentNullException(nameof(author));
            }
            _context.Authors.Remove(author);
        }

        public void DeleteCourse(Course course)
        {
            _context.Courses.Remove(course);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {            
            }
        }

        public Author GetAuthor(Guid authorId)
        {
            if (authorId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(authorId));
            }
            return _context.Authors.FirstOrDefault(a => a.Id == authorId);
        }


        public IEnumerable<Author> GetAuthors(string mainCategory, string searchQuery)
        {
            if( string.IsNullOrWhiteSpace(mainCategory) && string.IsNullOrWhiteSpace(searchQuery))
            {
                return GetAuthors();
            }

            var collection = _context.Authors as IQueryable<Author>;

            if (!string.IsNullOrWhiteSpace(mainCategory))
            {
                mainCategory = mainCategory.Trim();
                collection = collection.Where(a => a.MainCategory == mainCategory);
            }

            if (!string.IsNullOrWhiteSpace(searchQuery))
            {
                searchQuery = searchQuery.Trim();
                collection = collection.Where(a => 
                a.MainCategory.Contains(searchQuery) || 
                a.FirstName.Contains(searchQuery) || 
                a.LastName.Contains(searchQuery));
            }
            return collection.ToList();
        }



        public IEnumerable<Author> GetAuthors()
        {
            return _context.Authors.ToList<Author>();
        }

        public IEnumerable<Author> GetAuthors(IEnumerable<Guid> authorIds)
        {
            if (authorIds == null)
            {
                throw new ArgumentNullException(nameof(authorIds));
            }
            return _context.Authors.Where(a => authorIds.Contains(a.Id))
                .OrderBy(a => a.FirstName)
                .OrderBy(a => a.LastName)
                .ToList();
        }

        public Course GetCourse(Guid authorId, Guid courseId)
        {
            if (authorId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(authorId));
            }

            if (courseId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(courseId));
            }

            return _context.Courses
                .Where(c => c.AuthorId == authorId && c.Id == courseId).FirstOrDefault();
        }

        public IEnumerable<Course> GetCourses(Guid authorId)
        {
            if (authorId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(authorId));
            }
            return _context.Courses
                        .Where(c => c.AuthorId == authorId)
                        .OrderBy(c => c.Title).ToList();
        }

        public bool Save()
        {
            return (_context.SaveChanges() >= 0);
        }

        public void UpdateAuthor(Author author)
        {
            throw new NotImplementedException();
        }

        public void UpdateCourse(Course course)
        {
            throw new NotImplementedException();
        }
    }
}
