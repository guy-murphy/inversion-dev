using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ClariusLabs.NuDoc;

namespace Inversion.Documentation.Generator {
	public class MarkdownVisitor: Visitor {
		private readonly StringBuilder _builder = new StringBuilder();

		public string Markdown {
			get { return _builder.ToString(); }
		}

		private TypeDeclaration _currentType;

		public override void VisitMethod(Method method) {
			_builder.AppendLine();
			string methodName = (_currentType != null) ? method.Id.Replace(_currentType.Id.Substring(1), "").Substring(1) : method.Id;
			_builder.AppendFormat("### `{0}`", methodName);

			base.VisitMember(method);
		}

		public override void VisitProperty(Property property) {
			string propertyName = (_currentType != null) ? property.Id.Replace(_currentType.Id.Substring(1), "") : property.Id;
			if (propertyName.StartsWith("P.")) {
				_builder.AppendFormat("### `{0}`", propertyName.Replace("P.", "."));
			} else {
				_builder.AppendFormat("## `{0}`", propertyName);
			}

			base.VisitMember(property);
		}

		public override void VisitMember(Member member) {
			_builder.AppendLine();
			_builder.AppendFormat("### `{0}`", member.Id);

			base.VisitMember(member);
		}

		public override void VisitType(TypeDeclaration member) {
			_currentType = member;
			_builder.AppendLine();
			_builder.AppendFormat("## `{0}`", member.Id);

			base.VisitMember(member);
		}

		public override void VisitSummary(Summary summary) {
			_builder.AppendLine();
			base.VisitSummary(summary);
			_builder.AppendLine();
			_builder.AppendLine();
		}

		public override void VisitRemarks(Remarks remarks) {
			_builder.AppendLine("#### Remarks");
			base.VisitRemarks(remarks);
			_builder.AppendLine();
		}

		public override void VisitExample(Example example) {
			_builder.AppendLine("##### Example");
			base.VisitExample(example);
			_builder.AppendLine();
		}

		public override void VisitAssembly(AssemblyMembers assembly) {
			base.VisitAssembly(assembly);
		}

		public override void VisitDocument(DocumentMembers document) {
			base.VisitDocument(document);
		}

		public override void VisitDescription(Description description) {
			base.VisitDescription(description);
		}

		public override void VisitC(C code) {
			_builder.AppendFormat("`{0}`", code.Content);

			base.VisitC(code);
		}

		public override void VisitCode(Code code) {
			_builder.AppendLine();
			// Indent code with 4 spaces according to Markdown syntax.
			foreach (var line in code.Content.Split(new[] { Environment.NewLine }, StringSplitOptions.None)) {
				_builder.Append("    ");
				_builder.AppendLine(line);
			}
			base.VisitCode(code);

		}

		public override void VisitText(Text text) {
			_builder.Append(text.Content);

			base.VisitText(text);
		}

		public override void VisitPara(Para para) {
			// Avoid double line breaks between adjacent <para> elements.
			if (_builder.Length < 2 ||
				new string(new char[] { _builder[_builder.Length - 2], _builder[_builder.Length - 1] }) != Environment.NewLine) {
				_builder.AppendLine();
			}
			base.VisitPara(para);
			_builder.AppendLine();
			_builder.AppendLine();
		}

		private string _normalizeLink(string cref) {
			return cref.Replace(":", "-").Replace("(", "-").Replace(")", "");
		}

		public override void VisitSee(See see) {
			if (see.Cref != null) {
				var cref = _normalizeLink(see.Cref);
				_builder.AppendFormat(" [{0}]({1}) ", cref.Substring(2), cref);
			}
		}

		public override void VisitSeeAlso(SeeAlso seeAlso) {
			var cref = _normalizeLink(seeAlso.Cref);
			_builder.AppendFormat("[{0}]({1})", cref.Substring(2), cref);
		}

		public override void VisitParamRef(ParamRef paramRef) {
			base.VisitParamRef(paramRef);

			if (!string.IsNullOrEmpty(paramRef.Name)) _builder.AppendFormat("`{0}`", paramRef.Name);
		}

		public override void VisitParam(Param param) {
			_builder.AppendFormat("* `{0}`: ", param.Name);
			base.VisitParam(param);
			_builder.AppendLine();
		}

		public override void VisitTypeParam(TypeParam param) {
			_builder.AppendFormat("`{0}`: ", param.Name);
			base.VisitTypeParam(param);
			_builder.AppendLine();
		}

		public override void VisitReturns(Returns returns) {
			_builder.AppendLine();
			_builder.AppendLine("**returns:** ");
			base.VisitReturns(returns);
			_builder.AppendLine();
			_builder.AppendLine();
		}

		public override void VisitValue(Value value) {
			_builder.AppendLine();
			_builder.Append("**value:** ");
			base.VisitValue(value);
			_builder.AppendLine();
		}
	}
}
