using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CouponRestAPI.Data;
using CouponRestAPI.Dtos;
using CouponRestAPI.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CouponRestAPI.Controllers
{
    [Route("api/coupons")]
    [ApiController]
    public class CouponsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ICouponRepo _repository;
        //constructor
        public CouponsController(ICouponRepo repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        // GET: api/coupons
        [HttpGet]
        public ActionResult<IEnumerable<CouponReadDto>> GetAllCoupons()
        {
            var coupons = _repository.GetAllCoupons();
            return Ok(_mapper.Map<IEnumerable<CouponReadDto>>(coupons));
        }

        // GET api/coupons/5
        [HttpGet("{id}", Name= "GetCoupon")]
        public ActionResult<CouponReadDto> GetCouponById(string id)
        {
            var coupon = _repository.GetCouponById(id);
            if (coupon == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<CouponReadDto>(coupon));
        }

        // POST api/coupons       
        [HttpPost()]
        public ActionResult<CouponReadDto> CreateCoupon(CouponCreateDto couponCreateDto)
        {
            var couponModel = _mapper.Map<Coupon>(couponCreateDto);
            var coupon = _repository.CreateCoupon(couponModel);
            return CreatedAtRoute("GetCoupon", new { id = coupon.Id.ToString()  }, _mapper.Map<CouponReadDto>(coupon));
        }

        // PUT api/coupons/{id}
        [HttpPut("{id}")]
        public ActionResult UpdateCoupon(string id, CouponUpdateDto couponUpdateDto)
        {
            var couponModelFromRepo = _repository.GetCouponById(id);

            if (couponModelFromRepo == null)
            {
                return NotFound();
            }

            _mapper.Map(couponUpdateDto, couponModelFromRepo);

            _repository.UpdateCoupon(id, couponModelFromRepo);

            var coupon = _repository.GetCouponById(id);
            return Ok(_mapper.Map<CouponReadDto>(coupon));
        }

        // PATCH api/coupons/{id}
        [HttpPatch("{id}")]
        public ActionResult PartialUpdateCoupon(string id, JsonPatchDocument<CouponUpdateDto> patchDocument)
        {
            var couponModelFromRepo = _repository.GetCouponById(id);

            if (couponModelFromRepo == null)
            {
                return NotFound();
            }

            var couponToPatch = _mapper.Map<CouponUpdateDto>(couponModelFromRepo);
            patchDocument.ApplyTo(couponToPatch, (Microsoft.AspNetCore.JsonPatch.Adapters.IObjectAdapter)ModelState);

            if (!TryValidateModel(couponToPatch))
            {
                return ValidationProblem(ModelState);
            }

            _mapper.Map(couponToPatch, couponModelFromRepo);

            _repository.UpdateCoupon(id, couponModelFromRepo);

            var coupon = _repository.GetCouponById(id);
            return Ok(_mapper.Map<CouponReadDto>(coupon));
        }

        // DELETE api/coupons/{id}
        [HttpDelete("{id}")]
        public ActionResult DeleteCoupon(string id)
        {
            var couponModelFromRepo = _repository.GetCouponById(id);

            if (couponModelFromRepo == null)
            {
                return NotFound();
            }

            _repository.DeleteCoupon(couponModelFromRepo);

            return NoContent();
        }
    }
}
