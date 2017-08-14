using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TutorOnline.Common
{
    public class ManagerStringCommon
    {
        public string addCategoriesSuccess = "Đã thêm môn học mới thành công.";
        public string deleteCategoriesSuccess = "Đã xóa môn học thành công.";
        public string updateCategoriesSuccess = "Đã thay đổi thông tin môn học thành công.";
        public string isExistCategoryName = "Tên môn học này đã tồn tại. Hãy nhập tên khác!";
        public string isExistSubjectIn = "Có tồn tại khóa học của môn học này. Hãy xóa khóa học trước!";

        public string addSubjectsSuccess = "Đã thêm khóa học mới thành công.";
        public string deleteSubjectsSuccess = "Đã xóa khóa học thành công.";
        public string updateSubjectSuccess = "Đã thay đổi thông tin khóa học thành công.";
        public string isExistSubjectName = "Tên khóa học này đã tồn tại. Hãy nhập tên khác!";
        public string isExistLessonIn = "Có tồn tại bài học của khóa học này. Hãy xóa bài học trước!";
        public string isExistMaterialInSub = "Có tồn tại tài liệu học tập của khóa học này. Hãy xóa tài liệu học tập trước!";
        public string isExistQuestionInSub = "Có tồn tại bài tập tham khảo của khóa học này. Hãy xóa bài tập tham khảo trước!";
        public string isExistStudentIn = "Đã có học viên đăng ký khóa học này. Không được phép xóa khóa học!";

        public string mustChangeCVStatus = "Hãy chọn trạng thái muốn thay đổi.";
        public string changeCVStatusSucessfully = "Đã thay đổi trạng thái CV thành công.";

        public string addLessonSuccess = "Đã thêm bài học thành công.";
        public string deleteLessonSuccess = "Đã xóa bài học thành công.";
        public string isExistLessonName = "Tên bài học này đã tồn tại. Hãy nhập tên khác!";
        public string updateLessonSuccess = "Đã thay đổi thông tin bài học thành công.";
        public string isExistMaterialIn = "Có tồn tại tài liệu học tập của bài học này. Hãy xóa tài liệu học tập trước!";
        public string isExistQuestionIn = "Có tồn tại bài tập của bài học này. Hãy xóa bài tập trước!";
        public string isExistCriteriaIn = "Có tồn tại tiêu chí đánh giá của bài học này. Hãy xóa tiêu chí đánh giá trước!";

        public string addMaterialInSubSuccess = "Đã thêm tài liệu cho khóa học thành công.";
        public string addMaterialInLesSuccess = "Đã thêm tài liệu cho bài học thành công.";
        public string deleteMaterialSuccessInSub = "Đã xóa tài liệu của khóa học thành công.";
        public string deleteMaterialSuccessInLes = "Đã xóa tài liệu của bài học thành công.";
        public string isExistMaterialNameLes = "Tên tài liệu này đã tồn tại trong bài học này. Hãy nhập tên khác!";
        public string isExistMaterialNameSub = "Tên tài liệu này đã tồn tại trong khóa học này. Hãy nhập tên khác!";
        public string updateMaterialSuccess = "Đã thay đổi thông tin của tài liệu thành công.";
        public string isExistAnswerIn = "Có tồn tại câu trả lời của câu hỏi này. Hãy xóa câu trả lời trước!";
        public string requireUploadFileInSub = "Hãy tải tệp tài liệu học tập lên cho khóa học!";
        public string requireUploadFileInLes = "Hãy tải tệp tài liệu học tập lên cho bài học";
        public string isNotSupportMaterialType = "Hiện tại hệ thống chỉ cho phép tải lên những loại tệp tin có đuôi .PDF, .DOCX, .MP3 . Vui lòng chuyển tệp tin của bạn sang định dạng cho phép hoặc tải lên tệp tin khác.";

        public string isExistQuestionName = "Nội dung câu hỏi đã tồn tại trong bài học này. Hãy nhập nội dung khác!";
        public string isExistQuestionLink = "Link bài tập tham khảo đã tồn tại trong khóa học này. Hãy nhập đường link khác!";
        public string addQuestionSuccess = "Đã thêm câu hỏi trắc nghiệm vào bài học thành công.";
        public string requireQuestionLink = "Hãy thêm link bài tập tham khảo cho khóa học!";
        public string addLinkQuestionSuccess = "Đã thêm link bài tập tham khảo vào khóa học thành công.";
        public string updateQuestionSuccess = "Đã thay đổi thông tin câu hỏi của bài học thành công.";
        public string deleteQuestionSuccess = "Đã xóa câu hỏi của bài học thành công.";
        public string deleteQuestionLinkSuccess = "Đã xóa bài tập tham khảo của khóa học thành công.";

        public string isExistAnswerContent = "Nội dung câu trả lời đã tồn tại trong câu hỏi này. Hãy nhập nội dung khác!";
        public string addAnswerSuccess = "Đã thêm câu trả lời cho câu trắc nghiệm thành công.";
        public string updateAnswerSuccess = "Đã thay đổi thông tin câu trả lời thành công.";
        public string deleteAnswerSuccess = "Đã xóa câu trả lời thành công.";

        public string isExistCriteriaName = "Nội dung tiêu chí này đã tồn tại trong bài học này. Hãy nhập nội dung khác!";
        public string isUsedCriteriaInTuFeDe = "Tiêu chí này đã được sử dụng để đánh giá học viên trong bài học. Không được phép xóa.";
        public string addCriteriaSuccess = "Đã thêm tiêu chí đánh giá học viên cho bài học thành công.";
        public string updateCriteriaSuccess = "Đã thay đổi nội dung tiêu chí đánh giá thành công.";
        public string deleteCriteriaSuccess = "Đã xóa tiêu chí đánh giá học viên của bài học thành công.";

        public string approvedTutorSubjectSuccess = "Đã phê duyệt khóa dạy cho gia sư thành công.";
        public string approvedPreTutorSuccess = "Đã phê duyệt cho ứng viên trở thành gia sư thành công.";
        public string rejectedPreTutorSuccess = "Đã từ chối ứng viên gia sư thành công.";
        public string rejectedTutorSubjectMoreSuccess = "Đã từ chối khóa dạy đăng ký thêm của gia sư thành công.";
        public string updateSalaryForTutorSuccess = "Tiền lương đã được cập nhật.";

        public string uriIsNotValid = "Link bài tập tham khảo không khả dụng. Hãy nhập lại!";

        public string vn = "Vietnam";
        public string foreign = "foreign";
    }
}
