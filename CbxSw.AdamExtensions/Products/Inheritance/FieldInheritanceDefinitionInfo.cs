using System;
using System.Collections.Generic;
using Adam.Core.DataMapper;
using Adam.Core.Fields;
using Adam.Pims.Core;
using Adam.Pims.Core.Configuration;
using LabelItemCollection = Adam.Core.LabelItemCollection;

namespace CbxSw.AdamExtensions.Products.Inheritance
{
	public class FieldInheritanceDefinitionInfo : IObjectId
	{
		public Guid FieldId { get; set; }
		public string Name { get; set; }
		public DataType DataType { get; set; }
		public Scope Scope { get; set; }
		public bool IsActive { get; set; }
		public Execution PropagationMode { get; set; }
		public ICollection<FieldInheritanceRight> Rights { get; set; }
		public string Label { get; set; }
		public LabelItemCollection Labels { get; set; }
		public object ObjectId => FieldId;
	}
}