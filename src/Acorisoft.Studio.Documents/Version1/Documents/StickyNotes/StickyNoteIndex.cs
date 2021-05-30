namespace Acorisoft.Studio.Documents.StickyNotes
{
    public class StickyNoteIndex : DocumentIndex
    {
        /// <summary>
        /// 获取或设置当前便签的颜色。
        /// </summary>
        public string Color { get; set; }
        
        /// <summary>
        /// 获取或设置一段摘要性文字，内容长度为[0,100]
        /// </summary>
        public string Summary { get; set; }
    }
}