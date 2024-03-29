﻿using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Reactive.Disposables;
using System.Windows;
using Entity.Persons;
using Microsoft.Win32;
using Reactive.Bindings;
using Usecase;
using WpfApp1.EditWindow;

namespace WpfApp1.MainWindow
{
    /// <summary>
    /// MainWindowのViewModel
    /// </summary>
    public partial class MainWindowV : INotifyPropertyChanged
    {
        #region Fields ----------------------------------------------------------------------------------------

        private readonly CompositeDisposable _disposables = new();

        private readonly MainWindowM _model;

        #endregion --------------------------------------------------------------------------------------------

        #region Constants -------------------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------

        #region Properties ------------------------------------------------------------------------------------

        /// <summary>
        /// 個人情報リストの件数を取得します。
        /// </summary>
        public ReadOnlyReactivePropertySlim<int> PersonsCount { get; }

        /// <summary>
        /// 個人情報リストを取得します。
        /// </summary>
        public ReadOnlyReactiveCollection<PersonVM> Persons { get; }

        /// <summary>
        /// 組織人員問題を取得します。
        /// </summary>
        public ReadOnlyReactivePropertySlim<string?> ProblemsInfo { get; }

        /// <summary>
        /// 組織構造を取得します。
        /// </summary>
        public ReadOnlyReactivePropertySlim<string?> OrganizationInfo { get; }

        #region Add Command

        private Command _addCommand;

        /// <summary>
        /// Add コマンド
        /// </summary>
        public Command AddCommand
        {
            get
            {
                _addCommand ??= new Command(new Action(() =>
                {
                    var edit = new EditWindowV(
                        new EditWindowM(
                            new NameVO(string.Empty, string.Empty),
                            new BirthdayVO(1900, 1, 1))
                        )
                    {
                        Owner = this,
                    };
                    edit.OnCompleted += Edit_OnCompletedAdd;
                    edit.ShowDialog();
                    edit.OnCompleted -= Edit_OnCompletedAdd;
                }));

                return _addCommand;
            }
        }

        #endregion

        #region Load Command

        private Command _loadCommand;

        /// <summary>
        /// Load コマンド
        /// </summary>
        public Command LoadCommand
        {
            get
            {
                _loadCommand ??= new Command(new Action(async () =>
                    {
                        var ofd = new OpenFileDialog
                        {
                            Filter = "DATファイル（*.dat）|*.dat",
                        };
                        if (ofd.ShowDialog() == true)
                        {
                            await _model.Load(ofd.FileName);
                        }
                    }));

                return _loadCommand;
            }
        }

        #endregion

        #region Save Command

        private Command _saveCommand;

        /// <summary>
        /// Save コマンド
        /// </summary>
        public Command SaveCommand
        {
            get
            {
                _saveCommand ??= new Command(new Action(async () =>
                    {
                        var dialog = new SaveFileDialog
                        {
                            Filter = "DATファイル（*.dat）|*.dat",
                        };

                        var result = dialog.ShowDialog() ?? false;

                        if (!result)
                        {
                            return;
                        }

                        await _model.Save(dialog.FileName);
                    }));

                return _saveCommand;
            }
        }

        #endregion

        #endregion --------------------------------------------------------------------------------------------

        #region Events ----------------------------------------------------------------------------------------

        /// <summary>
        /// 不要だが、バインドした際のメモリリーク対策のため固定追加（必須） INotifyPropertyChanged
        /// </summary>
#pragma warning disable CS0067 // イベント 'Sample.PropertyChanged' は使用されていません
        public event PropertyChangedEventHandler? PropertyChanged;
#pragma warning restore CS0067 // イベント 'Sample.PropertyChanged' は使用されていません

        #endregion --------------------------------------------------------------------------------------------

        #region Methods ---------------------------------------------------------------------------------------

        #region Methods - public ------------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------

        #region Methods - private -----------------------------------------------------------------------------

        private void RemovePerson(Person person)
        {
            _model.RemovePerson(person);
        }

        private void EditWindow_OnCompletedEdit(Person args)
        {
            _model.Update(args);
        }

        private void Edit_OnCompletedAdd(Person person)
        {
            _model.AddPerson(person);
        }

        private void Usecase_OnArisedProblems(OnArisedProblemsEventArgs obj)
        {
            _model.UpdateProglemInfo(obj);
        }

        private void Usecase_OnPersonUpdate(Entity.Persons.Person obj)
        {
            _model.UpdatePersons();
            _model.UpdateOrganizationStructure();
        }

        private void DisposeViewModelElement()
        {
            _disposables.Dispose();
            _model.Dispose();
        }

        #endregion --------------------------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------
    }
}
