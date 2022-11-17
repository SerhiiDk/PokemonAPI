using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace PockemonReviewApp.Controllers
{
    [ApiController]
    [Route("/api/v1/review/")]
    public class ReviewController : Controller
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly IMapper _mapper;
        private readonly IPokemonRepository _pokemonRepository;
        private readonly IReviewerRepository _reviewerRepository;

        public ReviewController(IReviewRepository reviewRepository, IMapper mapper,
             IPokemonRepository pokemonRepository, IReviewerRepository reviewerRepository)
        {
            _reviewRepository = reviewRepository;
            _mapper = mapper;
            _pokemonRepository = pokemonRepository;
            _reviewerRepository = reviewerRepository;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Review>))]
        public IActionResult GetReviews()
        {
            var pokemons =  _mapper.Map<List<ReviewDto>>(_reviewRepository.GetReviews());

            return Ok(pokemons);
        }

        [HttpGet("{reviewId}")]
        [ProducesResponseType(200, Type = typeof(Review))]
        [ProducesResponseType(400)]
        public IActionResult GetReview(int reviewId)
        {
            if(!_reviewRepository.ReviewExists(reviewId))
                return NotFound();

            var pokemon = _mapper.Map<ReviewDto>(_reviewRepository.GetReview(reviewId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(pokemon);
        }


        [HttpGet("pokemon/{pokeId}")]
        [ProducesResponseType(200, Type = typeof(Review))]
        [ProducesResponseType(400)]
        public IActionResult GetReviewsForPokemon(int pokeId)
        {
            var reviews = _mapper.Map<List<ReviewDto>>(_reviewRepository.GetReviewOfPokemon(pokeId));

            if (!ModelState.IsValid)
                return BadRequest();

            return Ok(reviews);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateReview([FromQuery] int reviewId, [FromQuery] int pokeId, [FromBody] ReviewDto reviewCreate)
        {
            if (reviewCreate == null)
                return BadRequest(ModelState);


            if (!_pokemonRepository.PokenmonExists(pokeId))
            {
                ModelState.AddModelError("","Pokemon is not exists");
                return StatusCode(422, ModelState);
                
            }

            if (!_reviewerRepository.ReviewExists(reviewId))
            {
                ModelState.AddModelError("", "Reviewer is not exists");
                return StatusCode(422, ModelState);

            }

            //checking for dublicate

            var reviews = _reviewRepository.GetReviews()
                .Where(r => r.Title.Trim().ToUpper() == reviewCreate.Title.Trim().ToUpper())
                .FirstOrDefault();

            if(reviews != null)
            {
                ModelState.AddModelError("", "Review is already exists");
                return StatusCode(422, ModelState);
            }


            var reviewMap = _mapper.Map<Review>(reviewCreate);

            

            if(!_reviewRepository.CreateReview(reviewMap, reviewId, pokeId))
            {
                ModelState.AddModelError("","Something went wrong while wrong");
            }

            return Ok("Successfully created");

        }

    }
}
