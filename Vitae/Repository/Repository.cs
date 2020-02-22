using Library.ViewModels;
using Microsoft.EntityFrameworkCore;
using Persistency.Data;
using Persistency.Poco;

using System;
using System.Collections.Generic;
using System.Linq;

namespace Library.Repository
{
    public class Repository
    {
        private readonly VitaeContext vitaeContext;

        public Repository(VitaeContext vitaeContext)
        {
            this.vitaeContext = vitaeContext;
        }

        public Curriculum GetCurriculum(Guid curriculumID)
        {
            var curriculum = vitaeContext.Curriculums
            .Include(c => c.Person)
            .Include(c => c.Person.PersonCountries).ThenInclude(pc => pc.Country)
            .Include(c => c.Person.Country)
            .Include(c => c.Person.Language)
            .Single(c => c.Identifier == curriculumID);

            return curriculum;
        }

        public PersonVM GetPersonVM(Guid curriculumID, string email = null)
        {
            var curriculum = GetCurriculum(curriculumID);
            var personVM = new PersonVM()
            {
                Birthday_Day = curriculum.Person?.Birthday.Value.Day ?? 1,
                Birthday_Month = curriculum.Person?.Birthday.Value.Month ?? 1,
                Birthday_Year = curriculum.Person?.Birthday.Value.Year ?? DateTime.Now.Year - 1,
                City = curriculum.Person?.City,
                CountryCode = curriculum.Person?.Country.CountryCode,
                Email = curriculum.Person?.Email ?? email,
                Firstname = curriculum.Person?.Firstname,
                Lastname = curriculum.Person?.Lastname,
                Gender = curriculum.Person?.Gender,
                LanguageCode = curriculum.Person?.Language.LanguageCode,
                MobileNumber = curriculum.Person?.MobileNumber,
                Street = curriculum.Person?.Street,
                StreetNo = curriculum.Person?.StreetNo,
                ZipCode = curriculum.Person?.ZipCode,
                State = curriculum.Person?.State,
                Nationalities = curriculum.Person?.PersonCountries?.OrderBy(pc => pc.Order)
                .Select(n => new NationalityVM { CountryCode = n.Country.CountryCode, Order = n.Order })
                .ToList() ?? new List<NationalityVM>() { new NationalityVM() { Order = 0 } }
            };

            return personVM;
        }
    }
}