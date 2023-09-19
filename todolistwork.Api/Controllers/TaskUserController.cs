using Microsoft.AspNetCore.Mvc;
using todolistwork.Core.Entities;
using todolistwork.Core.Models;
using todolistwork.Application.IService;
using todolistwork.Core.Unit;

namespace todolistwork.Api.Controllers
{
    [Route("api/tasks")]
    public class TaskUserController : BaseApiController
    {
        // GET: api/<ValuesController>
        const string  x = "70cc0519-b6f8-4436-8907-66fc13642bf5";
        private readonly ITaskUserService taskUserService;
        public TaskUserController(ITaskUserService taskUserService)
        {   
            this.taskUserService = taskUserService;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try {

               string userId = HttpContext.Items["Id"]?.ToString();
               //string userId = x;

                var result = await taskUserService.GetAllTaskUser(userId);
                return Ok(result);
            } catch (Exception ex)
            {
                return BadRequest(ex);

            }
        }
        [HttpGet("to-do")]
        public async Task<IActionResult> GetToDo()
        {
            try
            {

                string userId = HttpContext.Items["Id"]?.ToString();
                // string userId = x;
                var result = await taskUserService.GetTaskUserByNeedToDo(userId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);

            }
        }

    

    // GET api/<ValuesController>/5
    [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            try {


                string userId = HttpContext.Items["Id"]?.ToString();
                // string userId = x;

                var result = await taskUserService.GetTaskUserById(id,userId);
                string data = UnitCore.ResultSerialize<TaskUser>(result);

                return Ok(data);
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST api/<ValuesController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] TaskUserBody requestBody)
        {
            try {


                string userId = HttpContext.Items["Id"]?.ToString();
                // string userId = x;


                TaskUser taskUser = new TaskUser(requestBody, userId);
                var result = await taskUserService.AddTaskUser(taskUser);
                string data = UnitCore.ResultSerialize<TaskUser>(result);

                return Ok(data);
            }
            catch(Exception ex) {
                Console.WriteLine(ex.Message);
                return BadRequest(ex.Message);
            }

        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromBody] TaskUserBody requestBody,  string id)
        {
            try {


                string userId = HttpContext.Items["Id"]?.ToString();
                // string userId = x;


                TaskUser taskUser = new TaskUser(requestBody, userId, id);
                var result = await taskUserService.UpdateTaskUser(taskUser);
                string data = UnitCore.ResultSerialize<TaskUser>(result);

                return Ok(data);

            }
            catch(Exception ex) { 
                return BadRequest(ex.Message);
            }
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            try {

                string userId = HttpContext.Items["Id"]?.ToString();
                // string userId = x;
                var result = await taskUserService.DeleteTaskUser(id,userId);
                return Ok(result);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
