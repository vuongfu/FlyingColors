using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TutorOnline.DataAccess;

namespace TutorOnline.Business.Repository
{
    public class LearningMaterialRepository : BaseRepository
    {
        public IEnumerable<MaterialType> GetAllMaType()
        {
            var matype = _dbContext.MaterialTypes;
            return matype;
        }
        public IEnumerable<LearningMaterial> GetAllMaterial()
        {
            var materials = _dbContext.LearningMaterials.Include(x => x.Lesson).Include(x => x.MaterialType).Where(x => x.isActived == true);
            return materials;
        }
        public IEnumerable<LearningMaterial> GetMaCategory(int? id)
        {
            var materials = _dbContext.LearningMaterials.Include(x => x.Lesson).Include(x => x.MaterialType).Where(x => x.Lesson.Subject.CategoryId == id && x.isActived == true);
            return materials;
        }
        public LearningMaterial FindMaterial(int? id)
        {
            LearningMaterial material = _dbContext.LearningMaterials.Where(x => x.isActived == true && x.MaterialId == id).FirstOrDefault();
            return material;
        }
        public List<LearningMaterial> FindMaterials(int? id)
        {
            List<LearningMaterial> listMaterial = GetAllMaterial().Where(x => x.LessonId == id && x.isActived == true).ToList();
            return listMaterial;
        }
        public void AddMaterial(LearningMaterial material)
        {
            material.isActived = true;
            _dbContext.LearningMaterials.Add(material);
            _dbContext.SaveChanges();
        }
        public void EditMaterial(LearningMaterial material)
        {
            material.isActived = true;
            _dbContext.Entry(material).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }
        public void DeleteMaterial(int id)
        {
            _dbContext.LearningMaterials.Where(x => x.MaterialId == id && x.isActived == true).ToList().ForEach(x => x.isActived = false);
            _dbContext.SaveChanges();
        }

        public bool isExistsMaterialNameInLes(string name, int? id)
        {
            var material = _dbContext.LearningMaterials.Where(x => x.LessonId == id && x.isActived == true).FirstOrDefault(x => x.MaterialUrl == name);
            if (material == null)
                return false;
            else
                return true;
        }
        public bool isExistsMaterialNameInSub(string name, int? id)
        {
            var material = _dbContext.LearningMaterials.Where(x => x.SubjectId == id && x.isActived == true).FirstOrDefault(x => x.MaterialUrl == name);
            if (material == null)
                return false;
            else
                return true;
        }

        public bool isNotSupportMaterialType(string type)
        {
            var materialType = _dbContext.MaterialTypes.FirstOrDefault(x => x.MaterialTypeName == type);
            if (materialType != null)
                return false;
            else
                return true;
        }
        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}
