﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TutorOnline.DataAccess
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class TutorOnlineContext : DbContext
    {
        public TutorOnlineContext()
            : base("name=TutorOnlineContext")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Answer> Answers { get; set; }
        public virtual DbSet<AuditLog> AuditLogs { get; set; }
        public virtual DbSet<BackendUser> BackendUsers { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Criterion> Criteria { get; set; }
        public virtual DbSet<CV> CVs { get; set; }
        public virtual DbSet<LearningMaterial> LearningMaterials { get; set; }
        public virtual DbSet<Lesson> Lessons { get; set; }
        public virtual DbSet<MaterialType> MaterialTypes { get; set; }
        public virtual DbSet<Parent> Parents { get; set; }
        public virtual DbSet<Question> Questions { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Schedule> Schedules { get; set; }
        public virtual DbSet<Status> Status { get; set; }
        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<StudentFeedback> StudentFeedbacks { get; set; }
        public virtual DbSet<StudentSubject> StudentSubjects { get; set; }
        public virtual DbSet<Subject> Subjects { get; set; }
        public virtual DbSet<TeachSchedule> TeachSchedules { get; set; }
        public virtual DbSet<Transaction> Transactions { get; set; }
        public virtual DbSet<Tutor> Tutors { get; set; }
        public virtual DbSet<TutorFeedback> TutorFeedbacks { get; set; }
        public virtual DbSet<TutorFeedbackDetail> TutorFeedbackDetails { get; set; }
        public virtual DbSet<TutorSubject> TutorSubjects { get; set; }
    }
}
