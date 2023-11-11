using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Viking.Models.Sports;

public partial class conViking_Sports : DbContext
{
    public conViking_Sports()
    {
    }

    public conViking_Sports(DbContextOptions<conViking_Sports> options)
        : base(options)
    {
    }

    public virtual DbSet<Exercise> Exercises { get; set; }

    public virtual DbSet<ExerciseDictionary> ExerciseDictionaries { get; set; }

    public virtual DbSet<Set> Sets { get; set; }

    public virtual DbSet<Workout> Workouts { get; set; }

    public virtual DbSet<WorkoutDictionary> WorkoutDictionaries { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Port=5439;Database=Viking.Sports;Username=Viking;Password=Viking;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Exercise>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Exercises_pkey");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.IdWorkoutNavigation).WithMany(p => p.Exercises)
                .HasForeignKey(d => d.IdWorkout)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_exercise_id");
        });

        modelBuilder.Entity<ExerciseDictionary>(entity =>
        {
            entity.HasKey(e => e.Code).HasName("ExerciseDictionary_pkey");

            entity.ToTable("ExerciseDictionary");

            entity.Property(e => e.Code).ValueGeneratedNever();
        });

        modelBuilder.Entity<Set>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Sets_pkey");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.IdExerciseNavigation).WithMany(p => p.Sets)
                .HasForeignKey(d => d.IdExercise)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_set_id");
        });

        modelBuilder.Entity<Workout>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Workout_pkey");

            entity.ToTable("Workout");

            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<WorkoutDictionary>(entity =>
        {
            entity.HasKey(e => e.Code).HasName("WorkoutDictionary_pkey");

            entity.ToTable("WorkoutDictionary");

            entity.Property(e => e.Code).ValueGeneratedNever();
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
