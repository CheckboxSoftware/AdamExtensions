Type.registerNamespace("CbxSw.Widgets");

CbxSw.Widgets.LoggedOnUsersWidget = function CbxSw$Widgets$LoggedOnUsersWidget(element) {
	CbxSw.Widgets.LoggedOnUsersWidget.initializeBase(this, [element]);

	this.__template = null;
	this.__searchExpression = "";
	this.__sortOrder = "";
	this.__maxItems = 5;
	this.__template = null;
	this.__dataKeys = null;
	this.__urlToItem = this.get_searchProvider();
};

function CbxSw$Widgets$LoggedOnUsersWidget$get_template() {
	return this.__template;
}
function CbxSw$Widgets$LoggedOnUsersWidget$set_template(value) {
	this.__template = value;
}

function CbxSw$Widgets$LoggedOnUsersWidget$get_urlToItem() {
	return this.__urlToItem;
}
function CbxSw$Widgets$LoggedOnUsersWidget$set_urlToItem(value) {
	this.__urlToItem = value;
}

function CbxSw$Widgets$LoggedOnUsersWidget$initialize() {
	jQuery(this.get_element()).addClass(this.get_searchProvider().toLowerCase() + "-widget contextsearch-widget");
	CbxSw.Widgets.LoggedOnUsersWidget.callBaseMethod(this, "initialize");
}

function CbxSw$Widgets$LoggedOnUsersWidget$_createItem(container, item) {
	var ui = jQuery("<a/>").addClass("context-item").attr({ href: this.get_url() + this.get_urlToItem() + "/~" + item.id });

	ui.appendTo(container);
	return jQuery("<div/>").addClass("context-item-content").appendTo(ui);
}

CbxSw.Widgets.LoggedOnUsersWidget.prototype = {
	get_template: CbxSw$Widgets$LoggedOnUsersWidget$get_template,
	set_template: CbxSw$Widgets$LoggedOnUsersWidget$set_template,
	get_urlToItem: CbxSw$Widgets$LoggedOnUsersWidget$get_urlToItem,
	set_urlToItem: CbxSw$Widgets$LoggedOnUsersWidget$set_urlToItem,

	initialize: CbxSw$Widgets$LoggedOnUsersWidget$initialize,

	_createItem: CbxSw$Widgets$LoggedOnUsersWidget$_createItem
};

CbxSw.Widgets.LoggedOnUsersWidget.registerClass("CbxSw.Widgets.LoggedOnUsersWidget", Adam.Studio.Widgets.ContextSearchWidget);
