using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VTT.models{
    //VTT.models.Article
    public class Article{
        [Key]
        public int Id { get; set;}
        [StringLength(255, MinimumLength = 5, ErrorMessage ="{0} phải dài tư {2} tới {1}") ]
        [Required(ErrorMessage ="{0} không được để trống")]
        [Column(TypeName ="nvarchar")]
        [DisplayName("Tiêu đề")]
        public string Title{ get; set;}

        [DataType(DataType.Date)]
        [Required (ErrorMessage ="{0} phải nhập")]
        [DisplayName("Ngày tạo")]
        public DateTime Created {get; set;}

        [Column(TypeName ="ntext")]
        [DisplayName("Nội dung")]
        public string Content {get; set;}
    }
}