using AnagramCheckerLogic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace APIAnagramChecker.Controllers
{
    public class Word
    {
        [Required]
        public string w1 { get; set; }

        [Required]
        public string w2 { get; set; }
    }

    [ApiController]
    [Route("api")]
    public class AnagramCheckerController : ControllerBase
    {
        private readonly IAnagramChecker checker;
        private readonly ILogger<AnagramCheckerController> logger;

        public AnagramCheckerController(IAnagramChecker checker, ILogger<AnagramCheckerController> logger)
        {
            this.checker = checker;
            this.logger = logger;
        }

        [HttpGet]
        [Route("checkAnagram")]
        public IActionResult CheckAnagram([FromBody] Word w)
        {
            if (checker.CompareTwoWords(w.w1, w.w2))
            {
                return Ok();
            }
            return BadRequest();
        }

        [HttpGet]
        [Route("getKnownAnagrams")]
        public async Task<ActionResult<string>> GetKnownAnagramsAsync([FromQuery] string w)
        {
            if (String.IsNullOrEmpty(w))
            {

                return BadRequest();
            }
            IEnumerable<string> anagrams = await checker.FindAnagramsAsync(w);

            if (anagrams.Count() != 0)
            {
                return Ok(anagrams);
            }

            logger.LogWarning("No anagrams found for " + w);
            return NotFound();
        }

        [HttpGet]
        [Route("getPermutations")]
        public ActionResult<string> GetPermutations([FromQuery] string w)
        {
            if (String.IsNullOrEmpty(w))
            {
                return BadRequest();
            }
            //Change to GetPermutations(w);
            IEnumerable<string> permutations = checker.GetPermutations(w);

            if (permutations.Count() != 0)
            {
                return Ok(permutations);
            }

            return NotFound();
        }
    }
}
