using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TutorOnline.DataAccess;
using System.Data.Entity;

namespace TutorOnline.Business.Repository
{
    public class AnswersRepository : BaseRepository
    {
        public List<Answer> GetAllAnswers(int? id)
        {
            var answer = _dbContext.Answers.Include(x => x.Question).Where(x => x.QuestionId == id && x.isActived == true).ToList();
            return answer;
        }
        public List<Answer> GetQuesAnswer(int? id)
        {
            var answer = _dbContext.Answers.Include(x => x.Question).Where(x => x.QuestionId == id && x.isActived == true).ToList();
            return answer;
        }
        public Answer FindAnswer(int? id)
        {
            Answer answer = _dbContext.Answers.Where(x => x.isActived == true && x.AnswerId == id).FirstOrDefault();
            return answer;
        }
        public void AddAnswer(Answer answer)
        {
            answer.isActived = true;
            _dbContext.Answers.Add(answer);
            _dbContext.SaveChanges();
        }
        public void EditAnswer(Answer answer)
        {
            answer.isActived = true;
            _dbContext.Entry(answer).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }
        public void DeleteAnswer(int id)
        {
            _dbContext.Answers.Where(x => x.AnswerId == id).ToList().ForEach(x => x.isActived = false);
            _dbContext.SaveChanges();
        }

        public bool isExistsAnswerName(string name, int id)
        {
            var answer = _dbContext.Answers.Where(x => x.QuestionId == id && x.isActived == true).FirstOrDefault(x => x.Content == name);
            if (answer == null)
                return false;
            else
                return true;
        }

        public bool isExistsAnswerNameEdit(string name, int id)
        {

            //Get anotherCategory
            var answer = _dbContext.Answers.Where(x => x.AnswerId != id && x.isActived == true);
            //Check isExistCategoryName
            foreach (var item in answer)
            {
                if (name != null)
                {
                    if (name.Trim() == item.Content)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}
