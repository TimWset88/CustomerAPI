using AutoMapper;
using CustomerApi.Data;
using CustomerApi.Models;
using CustomerApi.Repository;
using CustomerAPI.DTO;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text.Json;

namespace Commander.Controllers
{
    //api/commands
    [Route("api/customers")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ICustomerRepository _repository;
        private readonly CustomerContext _dbcontext;

        public CustomersController(ICustomerRepository repository,
                                    IMapper mapper, CustomerContext dbContext)
        {
            _mapper = mapper;
            _repository = repository;
            _dbcontext = dbContext;
        }
        

        //GET api/customers
        [HttpGet]
        public ActionResult<IEnumerable<Customer>> GetAllCustomers()
        {
            var customers = _repository.GetAllCustomers();
            return Ok(_mapper.Map<IEnumerable<CustomerReadDto>>(customers));
        }

        //GET api/customers/{id}
        [HttpGet("{id}", Name="GetCustomerById")]
        public ActionResult <CustomerReadDto> GetCustomerById(int id)
        {
            var customer = _repository.GetCustomerById(id);

            if (customer != null)
            { 
                return Ok(_mapper.Map<CustomerReadDto>(customer));
            }
            return NotFound();

        }

        [HttpPost]
        [Route("AddCustomer")]
        public ActionResult<CustomerReadDto> AddCustomer([FromBody] CustomerAddDto customerToAdd)
        {
            var customerModel = _mapper.Map<Customer>(customerToAdd);
            _repository.AddCustomer(customerModel);
            _dbcontext.SaveChanges();

            var customerReadDto = _mapper.Map<CustomerReadDto>(customerModel);

            return CreatedAtAction(nameof(GetCustomerById), new {Id = customerModel.Id}, customerReadDto);

        }

        [HttpPut ("{id}")]
        public ActionResult<CustomerUpdateDto> UpdateCustomer([FromBody] CustomerUpdateDto customerToUpdate, int id)
        {
            var customerFromRepository = _repository.GetCustomerById(id);

            if(customerFromRepository == null)
            {
                return NotFound();
            }

            var customerModel = _mapper.Map<Customer>(customerToUpdate);

            _mapper.Map<CustomerUpdateDto>(customerToUpdate);
            _repository.UpdateCustomer(customerModel);
            _dbcontext.SaveChanges();

            return NoContent();

        }

        [HttpPatch("{id}")]
        public ActionResult PatchCustomer(int id, JsonPatchDocument<CustomerUpdateDto> patchDoc)
        {
            var customerFromRepository = _repository.GetCustomerById(id);

            if (customerFromRepository == null)
            {
                return NotFound();
            }

            var customerToPatch = _mapper.Map<CustomerUpdateDto>(customerFromRepository);
            patchDoc.ApplyTo(customerToPatch, ModelState);

            if (!TryValidateModel(customerToPatch))
            {
                return ValidationProblem(ModelState);
            }

            _mapper.Map(customerToPatch, customerFromRepository);
            _repository.UpdateCustomer(customerFromRepository);
            _dbcontext.SaveChanges();

            return NoContent();
        }

    }
}
