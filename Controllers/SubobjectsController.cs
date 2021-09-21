using GoAndSee_API.Data;
using GoAndSee_API.Models;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace GoAndSee_API.Controllers
{

    public class SubobjectsController : ApiController
    {
        private ISubobjectDataAccess sobject = new SubobjectDataAccess();

        public  List<Subobject> Get()
        {
            List<Subobject> slist = sobject.readAllSubobject();

            return slist;
        }

        public List<Subobject> Get(string id)
        {
            List<Subobject> slist = sobject.readSubobjectbyOId(id);
            return slist;
        }

        public IHttpActionResult Post(Subobject subobject)
        {
            sobject.createSubobject(subobject);
            return Ok();
        }
    }
}
