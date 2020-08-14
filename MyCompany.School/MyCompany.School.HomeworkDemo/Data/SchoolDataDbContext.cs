using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace MyCompany.School.HomeworkDemo.Data
{
    public class SchoolDataDbContext : DbContext
    {
        public SchoolDataDbContext()
        {
        }

        public SchoolDataDbContext(DbContextOptions<SchoolDataDbContext> options)
            : base(options)
        {
        }
        public virtual DbSet<Branchs> Branchs { get; set; }
        public virtual DbSet<Classes> Classes { get; set; }
        public virtual DbSet<HomeworkDescriptions> HomeworkDescriptions { get; set; }
        public virtual DbSet<HomeworkFiles> HomeworkFiles { get; set; }
        public virtual DbSet<HomeworkTypes> HomeworkTypes { get; set; }
        public virtual DbSet<Lesson> Lessons { get; set; }
        public virtual DbSet<Note> Notes { get; set; }
        public virtual DbSet<PersonLessons> PersonLessons { get; set; }
        public virtual DbSet<Person> Persons { get; set; }
        public virtual DbSet<StudentPersonHomeworkFiles> StudentPersonHomeworkFiles { get; set; }
        public virtual DbSet<StudentPersonHomeworks> StudentPersonHomeworks { get; set; }
        public virtual DbSet<StudentPersonNotes> StudentPersonNotes { get; set; }
        public virtual DbSet<StudentPersons> StudentPersons { get; set; }
        public virtual DbSet<TeacherPersons> TeacherPersons { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=DESKTOP-2VP86G5\\SQLSERVER;Initial Catalog=SchoolDemo;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Branchs>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.BranchName)
                    .HasMaxLength(100)
                    .IsFixedLength();
            });

            modelBuilder.Entity<Classes>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();
                entity.Property(e => e.ClassName).HasMaxLength(10);
            });

            modelBuilder.Entity<HomeworkDescriptions>(entity =>
            {
                entity.Property(e => e.HomeworkDescription)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.LoadDate).HasColumnType("date");

                entity.HasOne(d => d.PersonLesson)
                    .WithMany(p => p.HomeworkDescriptions)
                    .HasForeignKey(d => d.PersonLessonId)
                    .HasConstraintName("FK_Homework_PersonLessons");
            });

            modelBuilder.Entity<HomeworkFiles>(entity =>
            {
                entity.HasKey(e => e.HomeworkId);

                entity.Property(e => e.HomeworkId).ValueGeneratedNever();

                entity.Property(e => e.FileId)
                    .HasMaxLength(10)
                    .IsFixedLength();
            });

            modelBuilder.Entity<HomeworkTypes>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.HomeworkType)
                    .HasMaxLength(100)
                    .IsFixedLength();
            });

            modelBuilder.Entity<Lesson>(entity =>
            {
                entity.Property(e => e.Name)
                    .HasMaxLength(30)
                    .IsFixedLength();
            });

            modelBuilder.Entity<Note>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();
            });

            modelBuilder.Entity<PersonLessons>(entity =>
            {
                entity.HasIndex(e => new { e.LessonId, e.PersonNo })
                    .HasName("UK_TeacherLesson_TeacherNo_LessonId")
                    .IsUnique();

                entity.HasOne(d => d.Lesson)
                    .WithMany(p => p.PersonLessons)
                    .HasForeignKey(d => d.LessonId)
                    .HasConstraintName("FK_BranchLessons_Lessons");

                entity.HasOne(d => d.PersonNoNavigation)
                    .WithMany(p => p.PersonLessons)
                    .HasForeignKey(d => d.PersonNo)
                    .HasConstraintName("FK_PersonLessons_Persons");
            });


            modelBuilder.Entity<StudentPersonHomeworkFiles>(entity =>
            {
                entity.HasKey(e => e.HomeworkId);

                entity.Property(e => e.HomeworkId).ValueGeneratedNever();

                entity.HasOne(d => d.Homework)
                    .WithOne(p => p.StudentPersonHomeworkFiles)
                    .HasForeignKey<StudentPersonHomeworkFiles>(d => d.HomeworkId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StudentPersonHomeworkFiles_StudentPersonHomeworks");
            });

            modelBuilder.Entity<StudentPersonHomeworks>(entity =>
            {
                entity.HasIndex(e => new { e.HomeworkId, e.StudentNo })
                    .HasName("UK_StudentPersonHomeworks_StudentNo_TLHNo")
                    .IsUnique();

                entity.Property(e => e.StudentAnswer)
                    .IsRequired()
                    .HasColumnType("text");

                entity.HasOne(d => d.Homework)
                    .WithMany(p => p.StudentPersonHomeworks)
                    .HasForeignKey(d => d.HomeworkId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StudentPersonHomeworks_Homework");

                entity.HasOne(d => d.StudentNoNavigation)
                    .WithMany(p => p.StudentPersonHomeworks)
                    .HasForeignKey(d => d.StudentNo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StudentPersonHomeworks_StudentPersons");
            });

            modelBuilder.Entity<StudentPersonNotes>(entity =>
            {
                entity.HasKey(e => e.NoteId);

                entity.Property(e => e.NoteId).ValueGeneratedNever();

                entity.HasOne(d => d.Note)
                    .WithOne(p => p.StudentPersonNotes)
                    .HasForeignKey<StudentPersonNotes>(d => d.NoteId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StudentPersonNotes_Notes");
            });

            modelBuilder.Entity<StudentPersons>(entity =>
            {
                entity.HasKey(e => e.PersonId);

                entity.HasIndex(e => e.StudentNo)
                    .HasName("UK_StudentPerson_StudentNo")
                    .IsUnique();

                entity.Property(e => e.PersonId).ValueGeneratedNever();

                entity.HasOne(d => d.Class)
                    .WithMany(p => p.StudentPersons)
                    .HasForeignKey(d => d.ClassId)
                    .HasConstraintName("FK_StudentPersons_Classes");

                entity.HasOne(d => d.Person)
                    .WithOne(p => p.StudentPersons)
                    .HasForeignKey<StudentPersons>(d => d.PersonId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StudentPersons_Persons");
            });

            modelBuilder.Entity<TeacherPersons>(entity =>
            {
                entity.HasKey(e => e.PersonId);

                entity.HasIndex(e => e.TeacherNo)
                    .HasName("UK_TeacherPersons_TeacherNo")
                    .IsUnique();

                entity.Property(e => e.PersonId).ValueGeneratedNever();

                entity.HasOne(d => d.Branch)
                    .WithMany(p => p.TeacherPersons)
                    .HasForeignKey(d => d.BranchId)
                    .HasConstraintName("FK_TeacherPersons_Branches");

                entity.HasOne(d => d.Person)
                    .WithOne(p => p.TeacherPersons)
                    .HasForeignKey<TeacherPersons>(d => d.PersonId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TeacherPersons_Persons1");
            });

            modelBuilder.Entity<Person>(entity =>
            {
                entity.HasKey(t => t.Id);
                entity.Property(t => t.Name).IsRequired();
                entity.Property(t => t.Surname).IsRequired();

            });
        }

    }
}
