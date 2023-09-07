﻿using System;
using Reactive.Bindings;

namespace Domain
{
    public class Person
    {
        #region Fields ----------------------------------------------------------------------------------------

        private readonly Name _name;
        private readonly Birthday _birthday;

        #endregion --------------------------------------------------------------------------------------------

        #region Constants -------------------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------

        #region Properties ------------------------------------------------------------------------------------

        public string Name { get { return _name.FullName; } }

        public int Age { get { return _birthday.Age; } }

        #endregion --------------------------------------------------------------------------------------------

        #region Events ----------------------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------

        #region Constructor -----------------------------------------------------------------------------------

        public Person(Name name, Birthday birthDay)
        {
            _name = name;
            _birthday = birthDay;
        }

        #endregion --------------------------------------------------------------------------------------------

        #region Methods ---------------------------------------------------------------------------------------

        #region Methods - public ------------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------

        #region Methods - private -----------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------
    }
}