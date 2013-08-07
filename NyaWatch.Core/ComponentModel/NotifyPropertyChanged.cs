﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace NyaWatch.Core.ComponentModel
{
    /// <summary>
    /// INotifyPropertyChanged implementation helper. Allows you to change underlying field
    /// and send notify event in one call. Avoids usage of strings for property names.
    /// </summary>
    public static class NotifyPropertyChanged
    {
        /// <summary>
        /// Change underlying field and notify event.
        /// </summary>
        /// <typeparam name="T">Type of value.</typeparam>
        /// <param name="handler">Event handler.</param>
        /// <param name="field">Underlying field reference.</param>
        /// <param name="value">New value.</param>
        /// <param name="memberExpression">Property reference (its name will be used for notification).</param>
        /// <returns>True if value changed, othrewise false.</returns>
        public static bool ChangeAndNotify<T>(
            this PropertyChangedEventHandler handler,
            ref T field, 
            T value, 
            Expression<Func<T>> memberExpression)
        {
            if (memberExpression == null)
            {
                throw new ArgumentNullException("memberExpression");
            }
            var body = memberExpression.Body as MemberExpression;
            if (body == null)
            {
                throw new ArgumentException("Lambda must return a property.");
            }
            if (EqualityComparer<T>.Default.Equals(field, value))
            {
                return false;
            }

            field = value;

            var vmExpression = body.Expression as ConstantExpression;
            if (vmExpression != null)
            {
                LambdaExpression lambda = Expression.Lambda(vmExpression);
                Delegate vmFunc = lambda.Compile();
                object sender = vmFunc.DynamicInvoke();

                if (handler != null)
                {
                    //handler(sender, new PropertyChangedEventArgs(body.Member.Name));
                    NotifyWithCaching(sender, handler, body.Member);
                }
            }

            return true;
        }


        static Dictionary<MemberInfo, PropertyChangedEventArgs> _cache = 
            new Dictionary<MemberInfo, PropertyChangedEventArgs>();

        static void NotifyWithCaching(object sender, PropertyChangedEventHandler handler, MemberInfo property)
        {
            try
            {
                lock (typeof(NotifyPropertyChanged))
                {
                    handler(sender, _cache[property]);
                }
            }
            catch (KeyNotFoundException)
            {
                var eventArgs = new PropertyChangedEventArgs(property.Name);
                handler(sender, eventArgs);
                lock (typeof(NotifyPropertyChanged))
                {
                    _cache[property] = eventArgs;
                }
            }
        }
    }
}
