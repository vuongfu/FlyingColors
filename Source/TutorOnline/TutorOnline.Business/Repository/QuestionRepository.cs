using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TutorOnline.DataAccess;
using System.Data.Entity;

namespace TutorOnline.Business.Repository
{
    public class QuestionRepository : BaseRepository
    {
        //public List<Question> GetAllLesQuestion(int? id)
        //{
        //    var question = _dbContext.Questions.Include(x => x.Lesson).Where(x => x.LessonId == id).ToList();
        //    return question;
        //}
        //public IEnumerable<Question> GetAllSubQuestion(int? id)
        //{
        //    var question = _dbContext.Questions.Include(x => x.Subject).Where(x => x.SubjectId == id).ToList();
        //    return question;
        //}
        //public Question FindQuestion(int? id)
        //{
        //    Question question = _dbContext.Questions.Find(id);
        //    return question;
        //}
        //public void AddQuestion(Question question)
        //{
        //    question.isActived = true;
        //    _dbContext.Questions.Add(question);
        //    _dbContext.SaveChanges();
        //}
        //public void EditQuestion(Question question)
        //{
        //    question.isActived = true;
        //    _dbContext.Entry(question).State = EntityState.Modified;
        //    _dbContext.SaveChanges();
        //}
        //public void DeleteQuestion(int id)
        //{
        //    _dbContext.Questions.Where(x => x.QuestionId == id).ToList().ForEach(x => x.isActived = false);
        //    _dbContext.SaveChanges();
        //}
        //public bool isExistedAnswerIn(int id)
        //{
        //    var answer = _dbContext.Answers.FirstOrDefault(x => x.QuestionId == id);
        //    if (answer == null)
        //        return false;
        //    else
        //        return true;
        //}
        //public bool isExistsQuestionName(string name, int id)
        //{
        //    var question = _dbContext.Questions.Where(x => x.LessonId == id).FirstOrDefault(x => x.Content == name);
        //    if (question == null)
        //        return false;
        //    else
        //        return true;
        //}
        //public void Dispose()
        //{
        //    _dbContext.Dispose();
        //}
    }
}
