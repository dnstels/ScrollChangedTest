﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interactivity;

namespace ScrollChangedTest.ViewModels
{
    public sealed class CustomCommandAction : TriggerAction<DependencyObject>
    {
        public static readonly DependencyProperty CommandParameterProperty =
            DependencyProperty.Register("CommandParameter", typeof(object), typeof(CustomCommandAction), null);

        public static readonly DependencyProperty CommandProperty = DependencyProperty.Register(
            "Command", typeof(ICommand), typeof(CustomCommandAction), null);

        public ICommand Command
        {
            get
            {
                return (ICommand)this.GetValue(CommandProperty);
            }
            set
            {
                this.SetValue(CommandProperty, value);
            }
        }

        public object CommandParameter
        {
            get
            {
                return GetValue(CommandParameterProperty);
            }

            set
            {
                SetValue(CommandParameterProperty, value);
            }
        }

        protected override void Invoke(object parameter)
        {
            if (this.AssociatedObject != null)
            {
                ICommand command = this.Command;
                if (command != null)
                {
                    if (this.CommandParameter != null)
                    {
                        if (command.CanExecute(this.CommandParameter))
                        {
                            command.Execute(this.CommandParameter);
                        }
                    }
                    else
                    {
                        if (command.CanExecute(parameter))
                        {
                            command.Execute(parameter);
                        }
                    }
                }
            }
        }
    }
}
