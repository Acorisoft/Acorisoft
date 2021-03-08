using LiteDB;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acorisoft.Morisa.ViewModels
{
    public class AppViewModel
    {
        public const string ApplicationDatabaseName = "AppSetting.Morisa-Setting";
        public const int ApplicationDatabaseSize = 4 * 1024 * 1024;
        public const string ConnectString = "FileName=AppSetting.Morisa-Setting;Initial Size = 4194304;Connection=Shared";

        [NonSerialized]
        private LiteDatabase _appDB;

        [NonSerialized]
        private SettingViewModel _settingVM;

        public AppViewModel()
        {
            //
            // 初始化设定部分
            OnInitializeSetting();
        }


        #region Setting Properties / Methods


        protected void OnInitializeSetting()
        {
            //
            // 创建视图模型
            //
            // 注意，这里可能会引发异常错误。
            try
            {
                _appDB = new LiteDatabase(ConnectString);

                //
                // 初始化设置视图模型
                _settingVM = new SettingViewModel(_appDB);
            }
            catch(Exception)
            {

            }
        }



        public SettingViewModel Setting => _settingVM;

        #endregion Setting Properties / Methods


        protected internal ILiteDatabase ApplicationDatabase => _appDB;

    }
}
