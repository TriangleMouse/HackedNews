using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HackedNews.Data.Models.NewsModel
{
    public class NewsData
    {
        public int Id { get; set; }
        [StringLength(120, ErrorMessage = "TitleLength", MinimumLength = 10)]
        public string Subtitle { get; set; }
        public string Img_Link { get; set; }// Превью новости(!Обязательно)
        public bool Switch_Load_Img { get; set; } //если тру, то ставим форму для дагрузки изображения. если фолсе,то ссылка
        public byte[] Img_Load { get; set; }
        //[Required(ErrorMessage = "TxtRequired")]
        [StringLength(5000, ErrorMessage = "TxtLength", MinimumLength = 50)]
        public string Txt { get; set; }

        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public IFormFile File { get; set; }

        public int NewsId { get; set; }
        public virtual News News { get; set; }

    }
}
