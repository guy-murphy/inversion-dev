using System;

namespace Inversion.Process {

	/// <summary>
	/// Represents a step in a rendering view pipeline.
	/// </summary>
	/// <remarks>
	/// A step can either have <see cref="Content"/> or
	/// a <see cref="Model"/>, but not both.
	/// </remarks>

	public class ViewStep {

		private readonly string _name;
		private readonly string _contentType;
		private readonly string _content;
		private readonly IData _model;

		/// <summary>
		/// The human readable name of the step.
		/// </summary>

		public string Name {
			get {
				return _name;
			}
		}

		/// <summary>
		/// The content type of the <see cref="Content"/>
		/// if there is any.
		/// </summary>

		public string ContentType {
			get {
				return _contentType;
			}
		}

		/// <summary>
		/// The content if any of the step.
		/// </summary>

		public string Content {
			get {
				return _content;
			}
		}

		/// <summary>
		/// The model if any of the step.
		/// </summary>
		public IData Model {
			get {
				return _model;
			}
		}

		/// <summary>
		/// Determines whether or not the step has any content.
		/// </summary>
		public bool HasContent {
			get {
				return !String.IsNullOrEmpty(this.Content);
			}
		}

		/// <summary>
		/// Determines whether or not the step has a model.
		/// </summary>
		public bool HasModel {
			get {
				return this.Model != null;
			}
		}

		/// <summary>
		/// Creates a new instance of a step with the parameters provided.
		/// </summary>
		/// <param name="name">Human readable name of the step.</param>
		/// <param name="contentType">The type of the steps content.</param>
		/// <param name="content">The actual content of the step.</param>

		public ViewStep(string name, string contentType, string content) {
			_name = name;
			_contentType = contentType;
			_content = content;
		}

		/// <summary>
		/// Creates a new instance of a step with the parameters provided.
		/// </summary>
		/// <param name="name">The human readable name of the step.</param>
		/// <param name="model">The actual model of the step.</param>

		public ViewStep(string name, IData model) {
			_name = name;
			_contentType = "model";
			_model = model;
		}
	}
}
