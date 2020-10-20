using Question.Domain.PostQuestionWorkflow;
using System;
using System.Collections.Generic;
using System.Net;
using static Question.Domain.PostQuestionWorkflow.PostQuestionResult;

namespace Test.App
{
    class ProgramQuestion
    {
        static void Main(string[] args)
        {
            List<string> tags = new List<string> {"CSS3", "SCSS"};
            var cmd = new PostQuestionCmd("How can we change bullet styling for an unordered list element?", "Can somebody give me a concrete example?", tags);
            var result = PostQuestion(cmd);

            result.Match(
                    ProcessQuestionPosted,
                    ProcessQuestionNotPosted,
                    ProcessInvalidQuestion
                );

            Console.ReadLine();
        }

        private static IPostQuestionResult ProcessInvalidQuestion(QuestionValidationFailed validationErrors)
        {
            Console.WriteLine("Validation failed: ");
            foreach (var error in validationErrors.ValidationErrors)
            {
                Console.WriteLine(error);
            }
            return validationErrors;
        }

        private static IPostQuestionResult ProcessQuestionNotPosted(QuestionNotPosted questionNotCreatedResult)
        {
            Console.WriteLine($"Question wasn't created: {questionNotCreatedResult.Reason}");
            return questionNotCreatedResult;
        }

        private static IPostQuestionResult ProcessQuestionPosted(QuestionPosted question)
        {
            Console.WriteLine($"Question {question.QuestionId}");
            return question;
        }

        public static IPostQuestionResult PostQuestion(PostQuestionCmd postQuestionCommand)
        {
            if (string.IsNullOrWhiteSpace(postQuestionCommand.Title))
            {
                var errors = new List<string>() { "Title is not valid" };
                return new QuestionValidationFailed(errors);
            }

            if (string.IsNullOrWhiteSpace(postQuestionCommand.Body))
            {
                var errors = new List<string>() { "Body is not valid" };
                return new QuestionValidationFailed(errors);
            }
      

            if (new Random().Next(10) > 1)
            {
                return new QuestionNotPosted("Question verification failed");
            }

            var questionId = Guid.NewGuid();
            var result = new QuestionPosted(questionId, postQuestionCommand.Title, postQuestionCommand.Body, postQuestionCommand.Tags);

            //execute logic
            return result;
        }
    }
}
