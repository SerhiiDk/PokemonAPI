namespace PockemonReviewApp.Controllers
{
    [ApiController]
    [Route("/api/v1/reviewer")]
    public class ReviewerController : Controller
    {
        private readonly IReviewerRepository _reviewerRepository;
        private readonly IMapper _mapper;

        public ReviewerController(IReviewerRepository reviewerRepository, IMapper mapper)
        {
            _reviewerRepository = reviewerRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerator<Reviewer>))]
        public IActionResult GetReviewers()
        {
            var reviewers = _mapper.Map<List<ReviewerDto>>(_reviewerRepository.GetReviewers());
            return Ok(reviewers);
        }


        [HttpGet("{reviwerId}/reviews")]
        [ProducesResponseType(200, Type = typeof(IEnumerator<Reviewer>))]
        [ProducesResponseType(400)]
        public IActionResult GetReviewerByAReviewer(int reviwerId)
        {
            if (!_reviewerRepository.ReviewExists(reviwerId))
            {
                return NotFound();
            }

            var reviews = _mapper.Map<List<ReviewDto>>(_reviewerRepository.GetReviewsByReviewer(reviwerId));

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            return Ok(reviews);
        }


        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateReviewer([FromBody] ReviewerDto reviewerCreate)
        {
            if (reviewerCreate == null)
                return BadRequest(ModelState);

            var reviewer = _reviewerRepository.GetReviewers()
                .Where(r => r.LastName.Trim().ToUpper() == reviewerCreate.LastName.Trim().ToUpper())
                .FirstOrDefault();

            if(reviewer != null)
            {
                ModelState.AddModelError("", "Reviewer is already exists");
                return StatusCode(500, ModelState);
            }


            var reviwerMap = _mapper.Map<Reviewer>(reviewerCreate);

            if(!_reviewerRepository.CreateReviewer(reviwerMap))
            {
                ModelState.AddModelError("", "Something went wront while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");

        }

        [HttpPut("{reviwerId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public IActionResult UpdateReviwer(int reviwerId, [FromBody] ReviewerDto updateReviwer)
        {
            if (updateReviwer == null)
                return BadRequest(ModelState);

            if (!_reviewerRepository.ReviewExists(reviwerId))
                return NotFound();

            var mapReviewer = _mapper.Map<Reviewer>(updateReviwer);
            if(!_reviewerRepository.UpdateReviewer(mapReviewer))
            {
                ModelState.AddModelError("", "Something went wrong while updating");
                return StatusCode(422, ModelState);
            }

            return Ok("Successfully updating");

        }
    }
}
