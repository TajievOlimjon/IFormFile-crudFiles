using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using WebCrudFiles.Data;
using WebCrudFiles.DTOs;
using WebCrudFiles.Responses;
using WebCrudFiles.Services;
using File = WebCrudFiles.Entities.File;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebCrudFiles.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly IFileService _fileService;
        private readonly ApplicationContext _context;
        private readonly IMapper _mapper;
        public FileController(IFileService fileService,ApplicationContext context,IMapper mapper)
        {
            _context = context;
            _fileService=fileService;
            _mapper = mapper;
        }
        // GET: api/<FileController>
        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            Response<List<GetAllFiles>> response;
            var files = _context.Files.ToList();
            if(files.Count is 0)
            {
                response = new Response<List<GetAllFiles>>
                {
                    Code = (int)HttpStatusCode.NoContent,
                    Message = "No content",
                    Data = _mapper.Map<List<GetAllFiles>>(new List<File>())
                };
                return BadRequest(response);
            }
           else{
                response = new Response<List<GetAllFiles>>
                {
                    Code = (int)HttpStatusCode.OK,
                    Message = "All data",
                    Data = _mapper.Map<List<GetAllFiles>>(files)
                };
                return Ok(response);
            }
        }
    

        // GET api/<FileController>/5
        [HttpGet("{id:int}")]
        public IActionResult GetById(int id)
        {
            Response<GetFileDto> response;
            var file = _context.Files.FirstOrDefault(x => x.Id.Equals(id));
            if(file is null)
            {
                response = new Response<GetFileDto>
                {
                    Code = (int)HttpStatusCode.NoContent,
                    Message = "No content",
                    Data = _mapper.Map<GetFileDto>(new File())
                };
                return BadRequest(response);

            }
            else
            {
                response = new Response<GetFileDto>
                {
                    Code = (int)HttpStatusCode.OK,
                    Message = "Success",
                    Data = _mapper.Map<GetFileDto>(file)
                };
                return BadRequest(response);
            }
        }

        // POST api/<FileController>
        [HttpPost("AddFile")]
        public IActionResult Add([FromForm] CreateFileDto fileDto)
        {
            Response<CreateFileDto> response;
            if(ModelState.IsValid is false)
            {
                ModelState.AddModelError("Custom errror","");
                return BadRequest(ModelState);
            }

            var file = _mapper.Map<File>(fileDto);

            file.FilePath = _fileService.AddFile(fileDto.FilePath);
            file.CreatedAd = DateTime.Now;
            _context.Files.Add(file);
            var x = _context.SaveChanges();

            if(x is 0)
            {
                response = new Response<CreateFileDto>
                {
                    Code = (int)HttpStatusCode.BadRequest,
                    Message = "No created !"
                };
                return BadRequest(response);
            }
            else
            {
                response = new Response<CreateFileDto>
                {
                    Code = (int)HttpStatusCode.OK,
                    Message = "Successfully added !",
                    Data=fileDto
                };
                return BadRequest(response);
            }
        }


        // PUT api/<FileController>/5
        [HttpPut("Update")]
        public IActionResult Update([FromForm] UpdateFileDto fileDto )
        {
            Response<UpdateFileDto> response;
            string filePath = "";

            if (ModelState.IsValid is false)
            {
                ModelState.AddModelError("Custom error","No updated");
                return BadRequest(ModelState);
            }
            else
            {
                var file = _context.Files.FirstOrDefault(x => x.Id.Equals(fileDto.Id));

                if(file is null)
                {
                    ModelState.AddModelError("Customerror", $"{fileDto.Id} not found");
                    return BadRequest(ModelState);
                 
                }
                else
                {
                    if (fileDto.FilePath is not null)
                    {
                        filePath = _fileService.UpdateFile(fileDto.FilePath, file.FilePath);
                    }
                    else
                    {
                        filePath = file.FilePath;
                    }

                    file.FilePath = filePath;
                    file.Name = fileDto.Name;
                    file.CreatedAd = file.CreatedAd;
                    file.UpdateAd = DateTime.Now;

                    _context.Files.Update(file);
                    var x = _context.SaveChanges();
                    if(x is 0)
                    {
                        response = new Response<UpdateFileDto>
                        {
                            Code = (int)HttpStatusCode.BadRequest,
                            Message = "File not updated!",
                            Data = fileDto
                        };
                        return BadRequest(response);
                    }
                    response = new Response<UpdateFileDto>
                    {
                        Code = (int)HttpStatusCode.OK,
                        Message = "Successfully updated !",
                        Data = fileDto
                    };
                    return Ok(response);
                }
                
               
            }
        }

        // DELETE api/<FileController>/5
        [HttpDelete("Delete")]
        public IActionResult Delete(int id)
        {
            Response<File> response; 
            if(id is 0)
            {
                ModelState.AddModelError("Customerror", $"{id} is null, invalid error!");
                return BadRequest(ModelState);
            }
            var file = _context.Files.FirstOrDefault(x => x.Id.Equals(id));
            if(file is null)
            {
                ModelState.AddModelError("Customerror", $"Not found!");
                return BadRequest(ModelState);
            }
            _context.Files.Remove(file);
            _fileService.DeleteFile(file.FilePath);
            var x= _context.SaveChanges();

            if (x is 0)
            {
                response = new Response<File>
                {
                    Code = (int)HttpStatusCode.BadRequest,
                    Message = "File not deleted!"
                };
                return BadRequest(response);
            }
            response = new Response<File>
            {
                Code = (int)HttpStatusCode.OK,
                Message = "Successfully deleted !",
                Data = file
            };
            return Ok(response);
        }
    }
}
