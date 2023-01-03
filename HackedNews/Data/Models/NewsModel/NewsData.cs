using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

namespace HackedNews.Data.Models.NewsModel
{
    public class NewsData
    {
        public int Id { get; set; }

        [StringLength(120, ErrorMessage = "TitleLength", MinimumLength = 10)]
        public string Subtitle { get; set; }

        public string ImgLink { get; set; } // Превью новости(!Обязательно)

        public bool SwitchLoadImg { get; set; } //если тру, то ставим форму для дагрузки изображения. если фолсе,то ссылка

        public byte[] ImgLoad { get; set; }

        //[Required(ErrorMessage = "TxtRequired")]
        [StringLength(5000, ErrorMessage = "TxtLength", MinimumLength = 50)]
        public string Txt { get; set; }

        [NotMapped] public IFormFile File { get; set; }

        public int NewsId { get; set; }
        public virtual News News { get; set; }
    }
}