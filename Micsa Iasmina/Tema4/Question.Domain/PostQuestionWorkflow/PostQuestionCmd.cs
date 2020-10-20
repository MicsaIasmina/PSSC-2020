using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Question.Domain.PostQuestionWorkflow
{
    // product type = Title*Body*Tags
    public struct PostQuestionCmd
    {
        [MaxLength(150)]
        [Required]
        public string Title { get; private set; } 
        [Required]
        public string Body { get; set; } 
        [Required]
        public List<string> Tags { get; set; } 
        
        public PostQuestionCmd(string questionTitle, string questionBody, List<string> questionTags)
        {
            Title = questionTitle;
            Body = questionBody;
            Tags = questionTags;
        }
    }
}
