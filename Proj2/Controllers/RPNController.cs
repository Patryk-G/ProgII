using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace Proj2class
{
    [ApiController]
    
    public class RPNController : ControllerBase
    {
        string v = "valid formula";
        string e = "error";
        string fill = "Please, enter the formula correctly";
        [HttpGet]
        [Produces("application/json")]
        [Route("api/tokens")]
        public IActionResult Get (string formula)
        {
            if (string.IsNullOrEmpty(formula))
            {
                var data = new
                {
                    status = e,
                    result = "enter the fomula"
                };
                return BadRequest(data);
            }
            RPN r = new RPN(formula);
            string[] infix = r.tokens(r.entry);
            if (!r.validation(infix))
            {
                var data = new
                {
                    status = e,
                    result = v
                };
                return BadRequest(data);
            }
            else
            {
                List<string> postfix = r.InfixToPostfix(infix);
                var temp = new
                {
                    infix = infix,
                    rpn = postfix
                };
                var data = new
                {
                    status = "ok",
                    result = temp
                };
                return Ok(data);
            }
        }

        [HttpGet]
        [Produces("application/json")]
        [Route("api/calculate")]
        public IActionResult Get (string formula, double x=double.NaN)
        {
            if (Double.IsNaN (x))
            {
                var data = new 
                {
                    status = e,
                    result = fill
                };
                return BadRequest(data);
            }
            if (string.IsNullOrEmpty(formula))
            {
                var data = new
                {
                    status = e,
                    result = fill
                };
                return BadRequest(data);
            }
            RPN r = new RPN(formula);
            r.variable = x;
            string[] infix = r.tokens(r.entry);
            if (!r.validation(infix))
            {
                var data = new 
                {
                    status = e,
                    result = v
                };
                return BadRequest(data);
            }
            else 
            {
                List<string> postfix = r.InfixToPostfix(infix);
                try
                {
                    var data = new
                    {
                        status = "ok",
                        result = r.InfixToPostfix_calc(postfix)
                    };
                    return Ok(data);
                }
                catch (Exception)
                {
                    var data = new
                    {
                        status = e,
                        result = "domain of definition"
                    };
                    return BadRequest(data);
                }
            };
        }
        /*
        [HttpGet]
        [Produces("application/json")]
        [Route("api/calculate/xy")]
        
        public IActionResult Get (string formula, double from=double.NaN, double to=double.NaN, double n=double.NaN) 
        {
            if (string.IsNullOrEmpty(formula))
            {
                var data = new
                {
                    status = e,
                    result = fill
                };
                return BadRequest(data);
            }
            if (Double.IsNaN (from))
            {
                var data = new 
                {
                    status = e,
                    result = fill
                };
                return BadRequest(data);
            }
            if (Double.IsNaN (to))
            {
                var data = new 
                {
                    status = e,
                    result = fill
                };
                return BadRequest(data);
            }
            if (Double.IsNaN (n))
            {
                var data = new 
                {
                    status = e,
                    result = fill
                };
                return BadRequest(data);
            }
            {
                RPN r = new RPN (formula);
                string [] infix = r.tokens(r.entry);
                if (!r.validation(infix))
                {
                    var data = new
                    {
                        status = e,
                        result = v
                    };
                return BadRequest(data);
                }
                else
                {
                List<string> asp = new List<string>();
                
                
                }
            }
        } 
        */
    }
}
