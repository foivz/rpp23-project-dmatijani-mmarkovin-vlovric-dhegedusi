﻿using DataAccessLayer.Repositories;
using EntitiesLayer;
using EntitiesLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLogicLayer.services {
    public class StatisticsService {
        public List<MostPopularBooks> GetMostPopularBooks(int Library_id) {
            using (var repo = new StatisticsRepository()) {
                var result = repo.GetMostPopularBooks(Library_id);
                return result;
            }
        }

        public List<MostPopularGenres> GetMostPopularGenres(int Library_id) {
            using (var repo = new StatisticsRepository()) {
                var result = repo.GetMostPopularGenres(Library_id);
                return result;
            }
        }
    }
}
