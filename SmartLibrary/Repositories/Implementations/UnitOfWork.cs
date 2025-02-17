using SmartLibrary.Models;
using SmartLibrary.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace SmartLibrary.Repositories.Implementations
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public IBookRepository BookRepository { get; }
        public ICategoryRepository CategoryRepository { get; }
        public IAuthorRepository AuthorRepository { get; }

        public IAuditLogRepository AuditLogRepository { get; }

        public IBorrowBookRepository BorrowBookRepository { get; }

        public IBookReservationRepository BookReservationRepository { get; }

        public IReportRepository ReportRepository { get; }

        public UnitOfWork(ApplicationDbContext context,
                          IBookRepository bookRepository,
                          ICategoryRepository categoryRepository,
                          IAuthorRepository authorRepository,
                          IAuditLogRepository auditLogRepository,
                          IBorrowBookRepository borrowBookRepository,
                          IBookReservationRepository bookReservationRepository,
                          IReportRepository reportRepository)
        {
            _context = context;
            BookRepository = bookRepository;
            CategoryRepository = categoryRepository;
            AuthorRepository = authorRepository;
            AuditLogRepository = auditLogRepository;
            BorrowBookRepository = borrowBookRepository;
            BookReservationRepository = bookReservationRepository;
            ReportRepository = reportRepository;
        }

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}