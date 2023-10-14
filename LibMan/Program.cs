using System;
using System.IO;

namespace Library
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create Files if not exists
            if (!File.Exists("users.txt"))
            {
                File.Create("users.txt").Close();
                File.AppendAllText("users.txt", "1,admin,admin,0");
            }
            if (!File.Exists("books.txt"))
                File.Create("books.txt").Close();
            if (!File.Exists("logins.txt"))
                File.Create("logins.txt").Close();
            if (!File.Exists("publishers.txt"))
                File.Create("publishers.txt").Close();
            if (!File.Exists("authors.txt"))
                File.Create("authors.txt").Close();
            if (!File.Exists("borrows.txt"))
                File.Create("borrows.txt").Close();

            System.Console.WriteLine("Welcome to the Library!");
            System.Console.WriteLine("You have to login first!");
            System.Console.WriteLine("Enter your username:");
            string username = Console.ReadLine();
            System.Console.WriteLine("Enter your password:");
            string password = Console.ReadLine();
            User user = UserContext.LoadUser(username, password);
            if (user == null)
            {
                System.Console.WriteLine("Wrong username or password!");
                Logger.loginLog(username, password, false);
            }
            else
                Logger.loginLog(username, password, true);

            while (true)
            {
                System.Console.WriteLine("Welcome " + user.Name);
                if (user.Role == Roles.Admin)
                {
                    // cast to admin
                    Admin admin = new Admin(user);
                    System.Console.WriteLine("1. Change your password");
                    System.Console.WriteLine("2. Add new librarian");
                    System.Console.WriteLine("3. Add new member");
                    System.Console.WriteLine("4. show all users");
                    System.Console.WriteLine("5. change users password by id");
                    System.Console.WriteLine("6. show a member info by id");
                    System.Console.WriteLine("7. show a librarian info by id");
                    System.Console.WriteLine("8. see all login attempts");
                    System.Console.WriteLine("9. see admin login attempts");
                    System.Console.WriteLine("0. Exit");

                    int choice = int.Parse(Console.ReadLine());

                    switch (choice)
                    {
                        case 1:
                            System.Console.WriteLine("Enter your new password:");
                            string newPassword = Console.ReadLine();
                            admin.ChangePassword(newPassword);
                            break;
                        case 2:
                            System.Console.WriteLine("Enter the new librarian's username:");
                            string newLibrarianUsername = Console.ReadLine();
                            System.Console.WriteLine("Enter the new librarian's password:");
                            string newLibrarianPassword = Console.ReadLine();
                            admin.AddLibrarian(newLibrarianUsername, newLibrarianPassword);
                            break;
                        case 3:
                            System.Console.WriteLine("Enter the new member's username:");
                            string newMemberUsername = Console.ReadLine();
                            System.Console.WriteLine("Enter the new member's password:");
                            string newMemberPassword = Console.ReadLine();
                            admin.AddMember(newMemberUsername, newMemberPassword);
                            break;
                        case 4:
                            admin.ShowAllUsers();
                            break;
                        case 5:
                            System.Console.WriteLine("Enter the user id:");
                            int userId = int.Parse(Console.ReadLine());
                            System.Console.WriteLine("Enter the new password:");
                            string newPassword2 = Console.ReadLine();
                            admin.ChangeUserPassword(userId, newPassword2);
                            break;
                        case 6:
                            System.Console.WriteLine("Enter the member id:");
                            int memberId = int.Parse(Console.ReadLine());
                            admin.ShowMemberInfo(memberId);
                            break;
                        case 7:
                            System.Console.WriteLine("Enter the librarian id:");
                            int librarianId = int.Parse(Console.ReadLine());
                            admin.ShowLibrarianInfo(librarianId);
                            break;
                        case 8:
                            Logger.showAllLoginAttempts();
                            break;
                        case 9:
                            Logger.showAdminLoginAttempts();
                            break;
                        case 0:
                            System.Console.WriteLine("Goodbye!");
                            return;
                        default:
                            System.Console.WriteLine("Wrong choice!");
                            break;
                    }
                }
                else if (user.Role == Roles.Librarian)
                {
                    // cast to librarian
                    Librarian librarian = new Librarian(user);
                    System.Console.WriteLine("1. Change your password");
                    System.Console.WriteLine("2. Add new author");
                    System.Console.WriteLine("3. Add new publisher");
                    System.Console.WriteLine("4. Add new book");
                    System.Console.WriteLine("5. show all books");
                    System.Console.WriteLine("6. show all books added by you");
                    System.Console.WriteLine("7. show all of your login attempts");
                    System.Console.WriteLine("0. Exit");

                    int choice = int.Parse(Console.ReadLine());

                    switch (choice)
                    {
                        case 1:
                            System.Console.WriteLine("Enter your new password:");
                            string newPassword = Console.ReadLine();
                            librarian.ChangePassword(newPassword);
                            break;
                        case 2:
                            System.Console.WriteLine("Enter the new author's name:");
                            string newAuthorName = Console.ReadLine();
                            System.Console.WriteLine("Enter the new author's phone number:");
                            string newAuthorPhoneNumber = Console.ReadLine();
                            librarian.AddAuthor(newAuthorName, newAuthorPhoneNumber);
                            break;
                        case 3:
                            System.Console.WriteLine("Enter the new publisher's name:");
                            string newPublisherName = Console.ReadLine();
                            System.Console.WriteLine("Enter the new publisher's phone number:");
                            string newPublisherPhoneNumber = Console.ReadLine();
                            librarian.AddPublisher(newPublisherName, newPublisherPhoneNumber);
                            break;
                        case 4:
                            System.Console.WriteLine("Enter the new book's name:");
                            string newBookName = Console.ReadLine();
                            System.Console.WriteLine("Enter the new book's author id:");
                            int newBookAuthorId = int.Parse(Console.ReadLine());
                            System.Console.WriteLine("Enter the new book's publisher id:");
                            int newBookPublisherId = int.Parse(Console.ReadLine());
                            System.Console.WriteLine("Enter the new book's base quantity:");
                            int newBookBaseQuantity = int.Parse(Console.ReadLine());
                            librarian.AddBook(newBookName, newBookAuthorId, newBookPublisherId, newBookBaseQuantity, librarian.id);
                            break;
                        case 5:
                            System.Console.WriteLine("1. show all books sorted by name");
                            System.Console.WriteLine("2. show all books sorted by quantity");

                            int choice2 = int.Parse(Console.ReadLine());

                            switch (choice2)
                            {
                                case 1:
                                    librarian.ShowAllBooksSortedByName();
                                    break;
                                case 2:
                                    librarian.ShowAllBooksSortedByQuantity();
                                    break;
                                default:
                                    System.Console.WriteLine("Wrong choice!");
                                    break;
                            }
                            break;
                        case 6:
                            librarian.ShowAllBooksAddedByYou();
                            break;
                        case 7:
                            Logger.showLoginAttempts(librarian);
                            break;
                        case 0:
                            System.Console.WriteLine("Goodbye!");
                            return;
                        default:
                            System.Console.WriteLine("Wrong choice!");
                            break;
                    }

                }
                else if (user.Role == Roles.Member)
                {
                    // cast to member
                    Member member = new Member(user);
                    System.Console.WriteLine("1. Change your password");
                    System.Console.WriteLine("2. Show all books");
                    System.Console.WriteLine("3. Borrow a book");
                    System.Console.WriteLine("4. Return a book");
                    System.Console.WriteLine("5. Show all borrowed books");
                    System.Console.WriteLine("6. Show all of your login attempts");
                    System.Console.WriteLine("0. Exit");

                    int choice = int.Parse(Console.ReadLine());

                    switch (choice)
                    {
                        case 1:
                            System.Console.WriteLine("Enter your new password:");
                            string newPassword = Console.ReadLine();
                            member.ChangePassword(newPassword);
                            break;
                        case 2:
                            System.Console.WriteLine("1. show all books sorted by name");
                            System.Console.WriteLine("2. show all books sorted by quantity");

                            int choice2 = int.Parse(Console.ReadLine());

                            switch (choice2)
                            {
                                case 1:
                                    member.ShowAllBooksSortedByName();
                                    break;
                                case 2:
                                    member.ShowAllBooksSortedByQuantity();
                                    break;
                                default:
                                    System.Console.WriteLine("Wrong choice!");
                                    break;
                            }
                            break;
                        case 3:
                            System.Console.WriteLine("Enter the book id:");
                            int bookId = int.Parse(Console.ReadLine());
                            System.Console.WriteLine("How many days do you want to borrow the book?");
                            int days = int.Parse(Console.ReadLine());
                            member.BorrowBook(bookId, days);
                            break;
                        case 4:
                            System.Console.WriteLine("Enter the book id:");
                            int bookId2 = int.Parse(Console.ReadLine());
                            member.ReturnBook(bookId2);
                            break;
                        case 5:
                            member.ShowAllBorrowedBooks();
                            break;
                        case 6:
                            Logger.showLoginAttempts(member);
                            break;
                        case 0:
                            System.Console.WriteLine("Goodbye!");
                            return;
                        default:
                            System.Console.WriteLine("Wrong choice!");
                            break;
                    }
                }
            }
        }
    }
    public enum Roles
    {
        Admin,
        Librarian,
        Member
    }

    public class User
    {
        private int _id;
        private string _name;
        private string _password;
        private Roles _role;
        public User(int id, string name, string password, Roles role)
        {
            _id = id;
            _name = name;
            _password = password;
            _role = role;
        }

        public User(string name, string password, Roles role)
        {
            _name = name;
            _password = password;
            _role = role;
        }

        public int id
        {
            get { return _id; }
            set { _id = value; }
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public string Password
        {
            get { return _password; }
            set { _password = value; }
        }

        public Roles Role
        {
            get { return _role; }
            set { _role = value; }
        }

        public override string ToString()
        {
            return $"{_id},{_name},{_password},{_role}";
        }

        internal void ShowAllBooksSortedByName()
        {
            Book[] books = BookContext.GetAllBooks();
            // sort books by name
            for (int i = 0; i < books.Length; i++)
            {
                for (int j = i + 1; j < books.Length; j++)
                {
                    if (books[i].title.CompareTo(books[j].title) > 0)
                    {
                        Book temp = books[i];
                        books[i] = books[j];
                        books[j] = temp;
                    }
                }
            }
            // print books
            for (int i = 0; i < books.Length; i++)
            {
                System.Console.WriteLine(i + 1 + ". " + books[i].ToString());
            }
        }

        internal void ShowAllBooksSortedByQuantity()
        {
            Book[] books = BookContext.GetAllBooks();
            // sort books by quantity
            for (int i = 0; i < books.Length; i++)
            {
                for (int j = i + 1; j < books.Length; j++)
                {
                    if (books[i].quantity < books[j].quantity)
                    {
                        Book temp = books[i];
                        books[i] = books[j];
                        books[j] = temp;
                    }
                }
            }
            // print books
            for (int i = 0; i < books.Length; i++)
            {
                System.Console.WriteLine(i + 1 + ". " + books[i].ToString());
            }
        }
    }

    public class Admin : User
    {
        public Admin(int id, string name, string password) : base(id, name, password, Roles.Admin)
        {
        }

        public Admin(string name, string password) : base(name, password, Roles.Admin)
        {
        }

        public Admin(User user) : base(user.id, user.Name, user.Password, Roles.Admin)
        {
        }

        internal void ChangePassword(string newPassword)
        {
            Password = newPassword;
            UserContext.UpdateUser(this);
        }

        internal void AddLibrarian(string newLibrarianUsername, string newLibrarianPassword)
        {
            Librarian librarian = new Librarian(newLibrarianUsername, newLibrarianPassword);
            UserContext.SaveUser(librarian);
        }

        internal void AddMember(string newMemberUsername, string newMemberPassword)
        {
            Member member = new Member(newMemberUsername, newMemberPassword);
            UserContext.SaveUser(member);
        }

        internal void ShowAllUsers()
        {
            User[] users = UserContext.GetAllUsers();
            foreach (User user in users)
            {
                System.Console.WriteLine(user);
            }
        }

        internal void ChangeUserPassword(int userId, string newPassword)
        {
            User user = UserContext.LoadUser(userId);
            user.Password = newPassword;
            UserContext.UpdateUser(user);
        }

        internal void ShowMemberInfo(int memberId)
        {
            // show all of this members borrows
            Member member = UserContext.LoadUser(memberId) as Member;
            var borrows = BorrowContext.LoadBorrow(member);

            foreach (Borrow borrow in borrows)
            {
                System.Console.WriteLine(borrow);
            }
        }

        internal void ShowLibrarianInfo(int librarianId)
        {
            // show all of this librarians actions
            Librarian librarian = UserContext.LoadUser(librarianId) as Librarian;
            var authors = AuthorContext.LoadAuthors(librarian.id);
            var publishers = PublisherContext.LoadPublishers(librarian.id);
            var books = BookContext.LoadBooks(librarian.id);

            System.Console.WriteLine("Authors:");
            foreach (Author author in authors)
            {
                System.Console.WriteLine(author);
            }

            System.Console.WriteLine("Publishers:");
            foreach (Publisher publisher in publishers)
            {
                System.Console.WriteLine(publisher);
            }

            System.Console.WriteLine("Books:");
            foreach (Book book in books)
            {
                System.Console.WriteLine(book);
            }
        }
    }

    public class Member : User
    {
        public Member(int id, string name, string password) : base(id, name, password, Roles.Member)
        {
        }

        public Member(string name, string password) : base(name, password, Roles.Member)
        {
        }

        public Member(User user) : base(user.id, user.Name, user.Password, Roles.Member)
        {
        }

        internal void ChangePassword(string newPassword)
        {
            Password = newPassword;
            UserContext.UpdateUser(this);
        }

        internal void BorrowBook(int bookId, int days)
        {
            Book book = BookContext.LoadBook(bookId);
            System.Console.WriteLine(book.id);
            if (book == null)
                throw new Exception("Book not found");
            Borrow borrow = new Borrow(this.id, bookId, DateTime.Now, days, false);
            BorrowContext.SaveBorrow(borrow);
            book.quantity--;
            BookContext.UpdateBook(book);
        }

        internal void ReturnBook(int bookId2)
        {
            Borrow[] borrows = BorrowContext.LoadBorrow(this);
            foreach (Borrow borrow in borrows)
            {
                if (borrow.Book.id == bookId2 && borrow.IsReturned == false)
                {
                    borrow.IsReturned = true;
                    BorrowContext.UpdateBorrow(borrow);
                    Book book = BookContext.LoadBook(bookId2);
                    book.quantity++;
                    BookContext.UpdateBook(book);
                    return;
                }
            }
            throw new Exception("Book not found");
        }

        internal void ShowAllBorrowedBooks()
        {
            Borrow[] borrows = BorrowContext.LoadBorrow(this);
            foreach (Borrow borrow in borrows)
            {
                if (borrow.IsReturned == false)
                {
                    System.Console.WriteLine(borrow.Book.title);
                }
            }
        }
    }

    public class Book
    {
        private int _id;
        private string _title;
        private Publisher _publisher;
        private Author _author;
        private int _quantity;
        private int _creatorId;

        public int id
        { get; private set; }

        public string title
        {
            get { return _title; }
            set { _title = value; }
        }

        public Publisher publisher
        {
            get { return _publisher; }
            set { _publisher = value; }
        }

        public Author author
        {
            get { return _author; }
            set { _author = value; }
        }

        public int quantity
        {
            get { return _quantity; }
            set { _quantity = value; }
        }

        public int creatorId
        {
            get { return _creatorId; }
            set { _creatorId = value; }
        }

        public Book(string title, int AuthorId, int PublisherId, int quantity, int creatorId)
        {
            this.id = BookContext.GetLastBookId() + 1;
            this.title = title;
            this.publisher = PublisherContext.LoadPublisher(PublisherId);
            this.author = AuthorContext.LoadAuthor(AuthorId);
            this.quantity = quantity;
            this.creatorId = creatorId;
        }

        public Book(int id, string title, int AuthorId, int PublisherId, int quantity, int creatorId)
        {
            this.id = id;
            this.title = title;
            this.publisher = PublisherContext.LoadPublisher(PublisherId);
            this.author = AuthorContext.LoadAuthor(AuthorId);
            this.quantity = quantity;
            this.creatorId = creatorId;
        }

        public override string ToString()
        {
            return this.title + " by " + this.author.name + " (" + this.publisher.name + ")" + " - " + this.quantity + " copies";
        }
    }

    public class Author
    {
        private int _id;
        private string _name;
        private string _phoneNumber;
        private int _creatorId;

        public int id
        { get; private set; }

        public string name
        {
            get { return _name; }
            set { _name = value; }
        }

        public string phoneNumber
        {
            get { return _phoneNumber; }
            set { _phoneNumber = value; }
        }

        public int creatorId
        {
            get { return _creatorId; }
            set { _creatorId = value; }
        }

        public Author(int id, string name, string phoneNumber, int creatorId)
        {
            this.id = id;
            this.name = name;
            this.phoneNumber = phoneNumber;
            this._creatorId = creatorId;
        }

        public Author(string name, string phoneNumber, int creatorId)
        {
            this.id = AuthorContext.GetLastAuthorId() + 1;
            this.name = name;
            this.phoneNumber = phoneNumber;
            this._creatorId = creatorId;
        }
    }

    public class Borrow
    {
        private int _id;
        private User _user;
        private Book _book;
        private DateTime _borrowDate;
        private DateTime _returnDate;
        private bool _isReturned;

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public User User
        {
            get { return _user; }
            set { _user = value; }
        }

        public Book Book
        {
            get { return _book; }
            set { _book = value; }
        }

        public DateTime BorrowDate
        {
            get { return _borrowDate; }
            set { _borrowDate = value; }
        }

        public DateTime ReturnDate
        {
            get { return _returnDate; }
            set { _returnDate = value; }
        }

        public bool IsReturned
        {
            get { return _isReturned; }
            set { _isReturned = value; }
        }

        public Borrow(int id, int userid, int bookid, DateTime borrowDate, DateTime returnDate, bool isReturned)
        {
            _id = id;
            _user = UserContext.LoadUser(userid);
            _book = BookContext.LoadBook(bookid);
            _borrowDate = borrowDate;
            _returnDate = returnDate;
            _isReturned = isReturned;
        }

        public Borrow(int userid, int bookid, DateTime borrowDate, int days, bool isReturned)
        {
            _id = BorrowContext.GetLastBorrowId() + 1;
            _user = UserContext.LoadUser(userid);
            _book = BookContext.LoadBook(bookid);
            _borrowDate = borrowDate;
            _returnDate = borrowDate.AddDays(days);
            _isReturned = isReturned;
        }

    }
    public class Librarian : User
    {
        public Librarian(int id, string name, string password) : base(id, name, password, Roles.Librarian)
        {
        }

        public Librarian(string name, string password) : base(name, password, Roles.Librarian)
        {
        }

        public Librarian(User user) : base(user.id, user.Name, user.Password, Roles.Librarian)
        {
        }

        internal void ChangePassword(string newPassword)
        {
            Password = newPassword;
            System.Console.WriteLine(this.Role);
            UserContext.UpdateUser(this);
        }

        internal void AddAuthor(string newAuthorName, string newAuthorPhoneNumber)
        {
            Author author = new Author(newAuthorName, newAuthorPhoneNumber, this.id);
            AuthorContext.SaveAuthor(author);
        }

        internal void AddPublisher(string newPublisherName, string newPublisherPhoneNumber)
        {
            Publisher publisher = new Publisher(newPublisherName, newPublisherPhoneNumber, this.id);
            PublisherContext.SavePublisher(publisher);
        }

        internal void AddBook(string newBookName, int newBookAuthorId, int newBookPublisherId, int newBookBaseQuantity, int id)
        {
            Book book = new Book(newBookName, newBookAuthorId, newBookPublisherId, newBookBaseQuantity, id);
            BookContext.SaveBook(book);
        }

        internal void ShowAllBooksAddedByYou()
        {
            Book[] books = BookContext.GetAllBooks();
            for (int i = 0; i < books.Length; i++)
            {
                if (books[i].creatorId == this.id)
                {
                    System.Console.WriteLine(i + 1 + ". " + books[i].ToString());
                }
            }
        }
    }
    public class Publisher
    {
        private int _id;
        private string _name;
        private string _phoneNumber;
        private int _creatorId;

        public int id
        { get; private set; }

        public string name
        {
            get { return _name; }
            set { _name = value; }
        }

        public string phoneNumber
        {
            get { return _phoneNumber; }
            set { _phoneNumber = value; }
        }

        public int creatorId
        {
            get { return _creatorId; }
            set { _creatorId = value; }
        }
        public Publisher(int id, string name, string phoneNumber, int creatorId)
        {
            this.id = id;
            this.name = name;
            this.phoneNumber = phoneNumber;
            this._creatorId = creatorId;
        }

        public Publisher(string name, string phoneNumber, int creatorId)
        {
            this.id = PublisherContext.GetLastPublisherId() + 1;
            this.name = name;
            this.phoneNumber = phoneNumber;
            this._creatorId = creatorId;
        }
    }

    public class AuthorContext
    {
        public static Author LoadAuthor(int id)
        {
            string[] lines = File.ReadAllLines("authors.txt");
            foreach (string line in lines)
            {
                string[] parts = line.Split(',');
                if (parts[0] == id.ToString())
                {
                    return new Author(int.Parse(parts[0]), parts[1], parts[2], int.Parse(parts[3]));
                }
            }
            return null;
        }

        public static Author[] LoadAuthors(int creatorid)
        {
            string[] lines = File.ReadAllLines("authors.txt");
            Author[] authors = new Author[lines.Length];
            int i = 0;
            foreach (string line in lines)
            {
                string[] parts = line.Split(',');
                if (parts[3] == creatorid.ToString())
                {
                    authors[i] = new Author(int.Parse(parts[0]), parts[1], parts[2], int.Parse(parts[3]));
                    i++;
                }
            }
            return authors;
        }

        public static int GetLastAuthorId()
        {
            string[] lines = File.ReadAllLines("authors.txt");
            if (lines.Length == 0)
                return 0;
            string lastLine = lines[lines.Length - 1];
            string[] parts = lastLine.Split(',');
            return int.Parse(parts[0]);
        }

        public static void SaveAuthor(Author author)
        {
            // check if author already exists
            string[] lines = File.ReadAllLines("authors.txt");
            foreach (string line in lines)
            {
                string[] parts = line.Split(',');
                if (parts[1] == author.name)
                {
                    throw new System.Exception("Author already exists!");
                }
            }

            // add author to file
            using (StreamWriter sw = File.AppendText("authors.txt"))
            {
                sw.WriteLine(author.id + "," + author.name + "," + author.phoneNumber, author.creatorId);
            }
        }
    }
    public class BookContext
    {
        public static Book LoadBook(int id)
        {
            string[] lines = File.ReadAllLines("books.txt");
            foreach (string line in lines)
            {
                string[] parts = line.Split(',');
                if (parts[0] == id.ToString())
                {
                    return new Book(int.Parse(parts[0]), parts[1], int.Parse(parts[2]), int.Parse(parts[3]), int.Parse(parts[4]), int.Parse(parts[5]));
                }
            }
            return null;
        }

        public static Book[] LoadBooks(int creatorid)
        {
            string[] lines = File.ReadAllLines("books.txt");
            Book[] books = new Book[lines.Length];
            int i = 0;
            foreach (string line in lines)
            {
                string[] parts = line.Split(',');
                if (parts[5] == creatorid.ToString())
                {
                    books[i] = new Book(int.Parse(parts[0]), parts[1], int.Parse(parts[2]), int.Parse(parts[3]), int.Parse(parts[4]), int.Parse(parts[5]));
                    i++;
                }
            }
            return books;
        }

        public static int GetLastBookId()
        {
            string[] lines = File.ReadAllLines("books.txt");
            if (lines.Length == 0)
                return 0;
            string lastLine = lines[lines.Length - 1];
            string[] parts = lastLine.Split(',');
            return int.Parse(parts[0]);
        }

        public static void SaveBook(Book book)
        {
            // check if book already exists
            string[] lines = File.ReadAllLines("books.txt");
            foreach (string line in lines)
            {
                string[] parts = line.Split(',');
                if (parts[1] == book.title)
                {
                    throw new System.Exception("Book already exists!");
                }
            }

            // add book to file
            using (StreamWriter sw = File.AppendText("books.txt"))
            {
                sw.WriteLine(book.id + "," + book.title + "," + book.publisher.id + "," + book.author.id + "," + book.quantity + "," + book.creatorId);
            }
        }

        public static Book[] GetAllBooks()
        {
            string[] lines = File.ReadAllLines("books.txt");
            Book[] books = new Book[lines.Length];
            for (int i = 0; i < lines.Length; i++)
            {
                string[] parts = lines[i].Split(',');
                books[i] = new Book(int.Parse(parts[0]), parts[1], int.Parse(parts[2]), int.Parse(parts[3]), int.Parse(parts[4]), int.Parse(parts[5]));
            }
            return books;
        }

        public static void UpdateBook(Book book)
        {
            string[] lines = File.ReadAllLines("books.txt");
            for (int i = 0; i < lines.Length; i++)
            {
                string[] parts = lines[i].Split(',');
                if (parts[0] == book.id.ToString())
                {
                    lines[i] = book.id + "," + book.title + "," + book.publisher.id + "," + book.author.id + "," + book.quantity + "," + book.creatorId;
                }
            }
            File.WriteAllLines("books.txt", lines);
        }
    }
    public class BorrowContext
    {
        public static Borrow LoadBorrow(int id)
        {
            string[] lines = File.ReadAllLines("borrows.txt");
            foreach (string line in lines)
            {
                string[] parts = line.Split(',');
                if (parts[0] == id.ToString())
                {
                    return new Borrow(int.Parse(parts[0]), int.Parse(parts[1]), int.Parse(parts[2]), DateTime.Parse(parts[3]), DateTime.Parse(parts[4]), bool.Parse(parts[5]));
                }
            }

            return null;
        }
        public static Borrow[] LoadBorrow(User user)
        {
            string[] lines = File.ReadAllLines("borrows.txt");
            Borrow[] borrows = new Borrow[lines.Length];
            int i = 0;
            foreach (string line in lines)
            {
                string[] parts = line.Split(',');
                if (parts[1] == user.id.ToString())
                {
                    borrows[i] = new Borrow(int.Parse(parts[0]), int.Parse(parts[1]), int.Parse(parts[2]), DateTime.Parse(parts[3]), DateTime.Parse(parts[4]), bool.Parse(parts[5]));
                    i++;
                }
            }
            return borrows;
        }

        internal static int GetLastBorrowId()
        {
            string[] lines = File.ReadAllLines("borrows.txt");
            if (lines.Length == 0)
                return 0;
            string lastLine = lines[lines.Length - 1];
            string[] parts = lastLine.Split(',');
            return int.Parse(parts[0]);
        }
        public static void SaveBorrow(Borrow borrow)
        {
            File.AppendAllText("borrows.txt", GetLastBorrowId() + 1 + "," + borrow.User.id + "," + borrow.Book.id + "," + borrow.BorrowDate + "," + borrow.ReturnDate + "," + borrow.IsReturned + "\n");
        }

        public static void UpdateBorrow(Borrow borrow)
        {
            string[] lines = File.ReadAllLines("borrows.txt");
            for (int i = 0; i < lines.Length; i++)
            {
                string[] parts = lines[i].Split(',');
                if (parts[0] == borrow.Id.ToString())
                {
                    lines[i] = borrow.Id + "," + borrow.User.id + "," + borrow.Book.id + "," + borrow.BorrowDate + "," + borrow.ReturnDate + "," + borrow.IsReturned;
                    break;
                }
            }
            File.WriteAllLines("borrows.txt", lines);
        }
    }
    public static class PublisherContext
    {
        public static Publisher LoadPublisher(int id)
        {
            string[] lines = File.ReadAllLines("publishers.txt");
            foreach (string line in lines)
            {
                string[] parts = line.Split(',');
                if (parts[0] == id.ToString())
                {
                    return new Publisher(int.Parse(parts[0]), parts[1], parts[2], int.Parse(parts[3]));
                }
            }
            return null;
        }

        public static Publisher[] LoadPublishers(int creatorid)
        {
            string[] lines = File.ReadAllLines("publishers.txt");
            Publisher[] publishers = new Publisher[lines.Length];
            int i = 0;
            foreach (string line in lines)
            {
                string[] parts = line.Split(',');
                if (parts[3] == creatorid.ToString())
                {
                    publishers[i] = new Publisher(int.Parse(parts[0]), parts[1], parts[2], int.Parse(parts[3]));
                    i++;
                }
            }
            return publishers;
        }

        public static int GetLastPublisherId()
        {
            string[] lines = File.ReadAllLines("publishers.txt");
            if (lines.Length == 0)
                return 0;
            string lastLine = lines[lines.Length - 1];
            string[] parts = lastLine.Split(',');
            return int.Parse(parts[0]);
        }

        public static void SavePublisher(Publisher publisher)
        {
            // check if publisher already exists
            string[] lines = File.ReadAllLines("publishers.txt");
            foreach (string line in lines)
            {
                string[] parts = line.Split(',');
                if (parts[1] == publisher.name)
                {
                    throw new System.Exception("Publisher already exists!");
                }
            }

            // add publisher to file
            using (StreamWriter sw = File.AppendText("publishers.txt"))
            {
                sw.WriteLine(publisher.id + "," + publisher.name + "," + publisher.phoneNumber, publisher.creatorId);
            }
        }
    }
    public static class UserContext
    {
        public static User LoadUser(int id)
        {
            string[] lines = File.ReadAllLines("users.txt");
            foreach (string line in lines)
            {
                string[] parts = line.Split(',');
                if (parts[0] == id.ToString())
                {
                    return new User(int.Parse(parts[0]), parts[1], parts[2], (Roles)int.Parse(parts[3]));
                }
            }

            return null;
        }

        public static User LoadUser(string name, string password)
        {
            string[] lines = File.ReadAllLines("users.txt");
            foreach (string line in lines)
            {
                string[] parts = line.Split(',');
                if (parts[1] == name && parts[2] == password)
                {
                    return new User(int.Parse(parts[0]), parts[1], parts[2], (Roles)int.Parse(parts[3]));
                }
            }

            return null;
        }

        public static int GetLastUserId()
        {
            string[] lines = File.ReadAllLines("users.txt");
            if (lines.Length == 0)
                return 0;
            string lastLine = lines[lines.Length - 1];
            string[] parts = lastLine.Split(',');
            return int.Parse(parts[0]);
        }

        public static void SaveUser(User user)
        {
            // check if username already exists
            string[] lines = File.ReadAllLines("users.txt");
            foreach (string line in lines)
            {
                string[] parts = line.Split(',');
                if (parts[1] == user.Name)
                {
                    throw new System.Exception("Username already exists!");
                }
            }
            // save user
            File.AppendAllText("users.txt", (GetLastUserId() + 1) + "," + user.Name + "," + user.Password + "," + (int)user.Role + "\n");
        }

        public static void UpdateUser(User user)
        {
            string[] lines = File.ReadAllLines("users.txt");
            for (int i = 0; i < lines.Length; i++)
            {
                string[] parts = lines[i].Split(',');
                if (int.Parse(parts[0]) == user.id)
                {
                    lines[i] = user.id + "," + user.Name + "," + user.Password + "," + (int)user.Role;
                    break;
                }
            }

            File.WriteAllLines("users.txt", lines);
        }

        public static User[] GetAllUsers()
        {
            string[] lines = File.ReadAllLines("users.txt");
            User[] users = new User[lines.Length];
            for (int i = 0; i < lines.Length; i++)
            {
                string[] parts = lines[i].Split(',');
                users[i] = new User(int.Parse(parts[0]), parts[1], parts[2], (Roles)int.Parse(parts[3]));
            }

            return users;
        }
    }

    public class Logger
    {
        public static void loginLog(string username, string password, bool status)
        {
            string log = $"{username},{password},{DateTime.Now},{status}";
            File.AppendAllText("login.txt", log + "\n");
        }

        public static void showAdminLoginAttempts()
        {
            // show admin login attempts
            string[] lines = File.ReadAllLines("login.txt");
            foreach (string line in lines)
            {
                string[] parts = line.Split(',');
                if (parts[0] == "admin")
                {
                    Console.WriteLine(line);
                }
            }
        }

        public static void showAllLoginAttempts()
        {
            // show all login attempts
            string[] lines = File.ReadAllLines("login.txt");
            foreach (string line in lines)
            {
                Console.WriteLine(line);
            }
        }

        public static void showLoginAttempts(User user)
        {
            // show login attempts for a specific user
            string[] lines = File.ReadAllLines("login.txt");
            foreach (string line in lines)
            {
                string[] parts = line.Split(',');
                if (parts[0] == user.Name)
                {
                    Console.WriteLine(line);
                }
            }
        }
    }
}

