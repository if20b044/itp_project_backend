using GoAndSee_API.Data;
using GoAndSee_API.Data.Access;
using GoAndSee_API.Data.Interface;
using GoAndSee_API.Models.DTO.Objects;
using System.Collections.Generic;
using System.Web.Http;
using Object = GoAndSee_API.Models.Object;

namespace GoAndSee_API.Controllers
{
    public class ObjectsController : ApiController
    {
        private IObjectDataAccess iobject = new ObjectDataAccess();
        private IRatingDataAccess irating = new RatingDataAccess();

        public List<ObjectListDTO> Get()
        {
            List<ObjectListDTO> olist = iobject.readAllObject();

            for (int i = 0; i < olist.Count; ++i)
            {
                olist[i].OlastAnswered = irating.readRatingDate(olist[i].Oid);
            }
            return olist;
        }

        public ObjectDTO Get(string id)
        {
            ObjectDTO @object = iobject.readObject(id);
            return @object;
        }

        public IHttpActionResult Post(Object @object)
        {
            iobject.createObject(@object);
            return Ok(@object.Oid);
        }

        public IHttpActionResult Delete(string id)
        {
            iobject.deleteObject(id);
            return Ok();
        }

        public IHttpActionResult Put(Object @object)
        {
            iobject.updateObject(@object);
            return Ok();
        }
    }
}
