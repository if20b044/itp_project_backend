﻿using GoAndSee_API.Business;
using GoAndSee_API.Data.Access;
using GoAndSee_API.Data.Interface;
using GoAndSee_API.Models;
using System.Collections.Generic;
using System.Web.Http;

namespace GoAndSee_API.Controllers
{
    public class RatingsController : ApiController
    {
        private IRatingDataAccess irating = new RatingDataAccess();
        private RatingAnswerProcessing rprocess = new RatingAnswerProcessing();

        public List<RatingDTO> Get()
        {
            List<RatingDTO> ratings = irating.readAllRating();

            foreach (RatingDTO rating in ratings)
            {
                rating.RQuestions = rprocess.prepareRatingAnswer(rating.Rid);
            }
            return ratings;
        }

        public RatingDTO Get(string id)
        {
            RatingDTO ratingDTO = irating.readRating(id);
            ratingDTO.RQuestions = rprocess.prepareRatingAnswer(id);

            return ratingDTO;
        }

        public IHttpActionResult Post(Rating rating)
        {
            irating.createRating(rating);
            irating.setRatingId(rating);
            rprocess.processRating(rating);

            return Ok(rating.Rid);
        }
    }
}
