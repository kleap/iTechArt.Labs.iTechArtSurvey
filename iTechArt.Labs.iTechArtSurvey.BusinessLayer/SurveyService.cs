﻿using System.Threading.Tasks;
using iTechArt.Labs.iTechArtSurvey.BusinessLayer.Abstractions;
using iTechArt.Labs.iTechArtSurvey.DataAccessLayer.DomainModel;
using iTechArt.Labs.iTechArtSurvey.DataAccessLayer.Repository;

namespace iTechArt.Labs.iTechArtSurvey.BusinessLayer
{
    public class SurveyService : ISurveyService
    {
        private readonly IRepository<Survey> _repository;
        public SurveyService(IRepository<Survey> repository)
        {
            _repository = repository;
        }

        public async Task CreateAsync(Survey survey)
        {
            await _repository.CreateAsync(survey).ConfigureAwait(false);
        }

        public async Task EditAsync(Survey survey)
        {
            await _repository.UpdateAsync(survey).ConfigureAwait(false);
        }

        public Task<Survey> GetAsync(int id)
        {
            return _repository.FindAsync(id);
        }
    }
}
