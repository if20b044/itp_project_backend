using GoAndSee_API.Models;
using System;
using System.Collections.Generic;

namespace GoAndSee_API.Data.Interface
{
    public interface IRatingDataAccess
    {
        RatingDTO readRating(string id);
        List<RatingDTO> readAllRating();
        void createRating(Rating rating);
        void setRatingId(Rating rating);
        DateTime readRatingDate(string id);
    }
}
