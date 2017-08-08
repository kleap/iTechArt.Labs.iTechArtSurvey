﻿using System.Data.Entity;
using iTechArt.Labs.iTechArtSurvey.DataAccessLayer.DomainModel;
using iTechArt.Labs.iTechArtSurvey.DataAccessLayer.EF.EntityConfigurations;

namespace iTechArt.Labs.iTechArtSurvey.DataAccessLayer.EF
{
    public class SurveyContext : DbContext
    {
        public DbSet<Role> Roles { get; set; }
        public DbSet<Survey> Surveys { get; set; }
        public DbSet<Template> Templates { get; set; }
        public DbSet<Reply> Replies { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new UserConfiguration());
            modelBuilder.Configurations.Add(new SurveyConfiguration());
            modelBuilder.Configurations.Add(new TemplateConfiguration());
            modelBuilder.Configurations.Add(new TemplateLookupConfiguration());
            modelBuilder.Configurations.Add(new UserReplyConfiguration());
            modelBuilder.Configurations.Add(new QuestionConfiguration());
            modelBuilder.Configurations.Add(new ReplyConfiguration());
            modelBuilder.Configurations.Add(new SurveyPageConfiguration());
            modelBuilder.Configurations.Add(new SurveyLookupConfiguration());
            modelBuilder.Configurations.Add(new SurveyPageQuestionConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
