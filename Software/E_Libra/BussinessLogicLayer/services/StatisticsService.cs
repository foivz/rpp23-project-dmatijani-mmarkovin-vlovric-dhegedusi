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
         public List<MostPopularBooks> GetMostPopularBooks() {
            using (var repo = new BookRepository()) {
                return repo.GetMostPopularBooks().ToList();
            }
        }
    }
}
