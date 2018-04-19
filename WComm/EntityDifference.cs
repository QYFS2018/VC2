using System;

namespace WComm
{
	/// <summary>
	/// The difference of to entity compare.
	/// </summary>
	public class EntityDifference
	{
		/// <summary>
		///		Constructor for EntityDifference class.
		/// </summary>
		/// <param name="_propertyName">The property name of difference.</param>
		/// <param name="_originalValue">The original value of this property.</param>
		/// <param name="_newValue">The new value of this property.</param>
		public EntityDifference(string _propertyName,object _originalValue,object _newValue)
		{
			propertyName = _propertyName;
			originalValue = _originalValue;
			newValue = _newValue;
			//
			// TODO: Add constructor logic here
			//
		}


		private string propertyName;
		/// <summary>
		///		The property name of difference.
		/// </summary>
		public string PropertyName
		{
			get
			{
				return propertyName;
			}
			set
			{
				propertyName = value;
			}
		}

		private object originalValue;
		/// <summary>
		///		The original value of this property.
		/// </summary>
		public object OriginalValue
		{
			get
			{
				return originalValue;
			}
			set
			{
				originalValue = value;
			}
		}

		private object newValue;
		/// <summary>
		///		The new value of this property.
		/// </summary>
		public object NewValue
		{
			get
			{
				return newValue;
			}
			set
			{
				newValue = value;
			}
		}
	}
}
