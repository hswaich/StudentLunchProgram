var ApplicationModel = function (appData) {
    var self = this;
    ko.mapping.fromJS(appData, {}, self);
    self.StepErrors = ko.observableArray();
    self.childFormEditMode = ko.observable(false);
    self.adultFormEditMode = ko.observable(false);

    //true means case number is required
    self.AssistanceProgramIsRequired = ko.computed(function () {
        if (self.AssistanceProgramId() == null || self.AssistanceProgramId() == self.AssistanceProgramNoneId())
        { return false; }
        else
        { return true; }
    }, this, { pure: true });

    self.AssistanceProgramIdChanged = function () {
        var IsValid = true;
        ErrorMessageClear("#CaseNumberFieldError");
        if (self.AssistanceProgramIsRequired() &&
            (self.AssistanceProgramCaseNumber() == null || self.AssistanceProgramCaseNumber() == "")) {
            IsValid = false;
            ErrorMessageAdd("#CaseNumberFieldError", "Case Number Field is required.");
        }
        return IsValid;
    };

    //self.AssistanceProgramSelectedName = ko.computed(function () {
    //    if (self.AssistanceProgramIsRequired()) {
    //        var programName = "";

    //        ko.utils.arrayForEach(self.AssistancePrograms(), function (program) {
    //            if (program.Value() == self.AssistanceProgramId()) {
    //                programName = program.Name();
    //            }
    //        });

    //        return programName;
    //    }
    //    else {
    //        return "";
    //    }
    //}, this, { pure: true });




    self.childMemberAdd = function () {
        $.getJSON('/Home/NewMember/1/' + self.Id(), function (json) {
            self.childFormEditMode(true);
            self.Children.push(json);
        });
    };

    self.childMemberEdit = function (member) {
        member.IsRowInEditMode(true);
        self.childFormEditMode(true);
    };

    self.childMemberRemove = function (member) {
        var jqxhr = $.post("/Home/MemberDelete", member, function (members) {
            ko.mapping.fromJS(members, {}, self.Children);
        }).always(function () {

        });
    };

    self.childMemberCancelEdit = function (member) {
        self.Children.remove(member);
        self.childFormEditMode(false);
    };

    self.childMemberSave = function (member) {
        var isValid1 = self.IsValidMemberFirstName(member);
        var isValid2 = self.IsValidMemberLastName(member);
        if (isValid1 && isValid2) {
            $.ajax({
                method: "POST",
                url: "/Home/MemberSave",
                data: ko.toJSON(member),
                dataType: "json",
                contentType: "application/json; charset=utf-8"
            }).done(function (members) {
                ko.mapping.fromJS(members, {}, self.Children);
                self.childFormEditMode(false); //success  
            });          
        }
    };

    self.IsNullOrEmpty = function (val) {
        return val == null || val == "";
    };

    self.ClearMemberNameError = function () {
        $(".FirstNameError").text("");
        $(".LastNameError").text("");
    };

    self.IsValidMemberFirstName = function (member) {
        var isInValid = member.IsNewRow.toString() == "true" ? self.IsNullOrEmpty(member.FirstName) : self.IsNullOrEmpty(member.FirstName());
        var message = isInValid ? "First Name is Required." : "";
        $(".FirstNameError").text(message);
        return !isInValid;
    };

    self.IsValidMemberLastName = function (member) {
        var isInValid = member.IsNewRow.toString() == "true" ? self.IsNullOrEmpty(member.LastName) : self.IsNullOrEmpty(member.LastName());
        var message = isInValid ? "Last Name is Required." : "";
        $(".LastNameError").text(message);
        return !isInValid;
    };

    self.adultMemberAdd = function () {
        $.getJSON('/Home/NewMember/2/' + self.Id(), function (json) {
            self.adultFormEditMode(true);
            self.Adults.push(json);
        });
    };

    self.adultMemberRemove = function (member) {
        var jqxhr = $.post("/Home/MemberDelete", member, function (members) {
            ko.mapping.fromJS(members, {}, self.Adults);
        }).always(function () { });
    };

    self.adultMemberEdit = function (member) {
        member.IsRowInEditMode(true);
        self.adultFormEditMode(true);
    };

    self.adultMemberCancelEdit = function (member) {
        self.Adults.remove(member);
        self.adultFormEditMode(false);
    };

    self.adultMemberSave = function (member) {
        var isValid1 = self.IsValidMemberFirstName(member);
        var isValid2 = self.IsValidMemberLastName(member);
        if (isValid1 && isValid2) {

            $.ajax({
                method: "POST",
                url: "/Home/MemberSave",
                data: ko.toJSON(member),
                dataType: "json",
                contentType: "application/json; charset=utf-8"
            }).done(function (members) {
                ko.mapping.fromJS(members, {}, self.Adults);
                self.adultFormEditMode(false); //success  
            });
        }
    };

    self.IsTotalMembersValid = function () {
        var IsValid = self.Children().length + self.Adults().length == self.TotalMembers();
        if (IsValid) {
            ErrorMessageClear("#TotalMembersFieldError");
        }
        else {
            var message = "Total does not match. Total Children: " + self.Children().length + " and Total Adults: " + self.Adults().length;
            ErrorMessageAdd("#TotalMembersFieldError", message);
        }
        return IsValid;
    };

    self.CalculateTotalIfEmpty = function () {
        if (self.IsNullOrEmpty(self.TotalMembers())) {
            self.TotalMembers(self.Children().length + self.Adults().length);
        }
    };

    self.NoSsnIsChecked = ko.computed(function () {
        if (self.NoSSN()) {
            ErrorMessageClear("#MemberSSNFieldError");
            self.MemberLastFourSSN("");
        }
        return self.NoSSN();
    }, this);

    self.ssnChanged = function () {
        self.ssnRequiredAndIsInvalid();
    };

    self.ssnRequiredAndIsInvalid = function () {
        var isSSNValid = false;

        if (!self.NoSSN() && (self.MemberLastFourSSN() == null || self.MemberLastFourSSN() == "")) {
            ErrorMessageAdd("#MemberSSNFieldError", "This field is required.");
        }
        else if (!self.NoSSN() && self.MemberLastFourSSN().toString().length != 4) {
            ErrorMessageAdd("#MemberSSNFieldError", "Please enter 4 digits.");
        }
        else {
            isSSNValid = !self.NoSSN();
            ErrorMessageClear("#MemberSSNFieldError");
        }
        return isSSNValid;
    };

    self.IsIncomeComplete = function (isChild) {
        var isComplete = true;
        if (isChild) {
            ko.utils.arrayForEach(self.Children(), function (member) {
                if (!member.IsIncomeCompleted()) {
                    isComplete = false;
                }
            });
        }
        else {
            ko.utils.arrayForEach(self.Adults(), function (member) {
                if (!member.IsIncomeCompleted()) {
                    isComplete = false;
                }
            });
        }
        return isComplete;
    };

    self.IsCityValid = function () {
        var IsValid = true;
        if (self.City() == null || self.City() == "") {
            IsValid = false;
            ErrorMessageAdd("#CityFieldError", "Required.");
        }
        else {
            ErrorMessageClear("#CityFieldError");
        }
        return IsValid;
    };

    self.IsStateValid = function () {
        var IsValid = true;
        if (self.State() == null || self.State() == "") {
            IsValid = false;
            ErrorMessageAdd("#StateFieldError", "Required.");
        }
        else {
            ErrorMessageClear("#StateFieldError");
        }
        return IsValid;
    };

    self.IsZipValid = function () {
        var IsValid = true;
        if (self.Zip() == null || self.Zip() == "") {
            IsValid = false;
            ErrorMessageAdd("#ZipFieldError", "Required.");
        }
        else {
            ErrorMessageClear("#ZipFieldError");
        }
        return IsValid;
    };

    self.IsAdultFilledByNameValid = function () {
        var IsValid = true;
        if (self.AdultFilledByName() == null || self.AdultFilledByName() == "") {
            IsValid = false;
            ErrorMessageAdd("#AdultFilledByNameFieldError", "Required.");
        }
        else {
            ErrorMessageClear("#AdultFilledByNameFieldError");
        }
        return IsValid;
    };

    self.step1Continue = function () {
        self.GoToStep(self.StepForward(1));
    };

    self.step2Continue = function () {
        var postData = { AppId: self.Id(), Id: self.AssistanceProgramId(), CaseNumber: self.AssistanceProgramCaseNumber() };
        var jqxhr = $.post("/Home/SaveAssistanceProgram", postData, function (data) { })
          .always(function () {
              self.GoToStep(self.StepForward(2));
          });
    };

    self.step3Continue = function () {
        if (self.IsStep3Valid()) {
            var postData = { AppId: self.Id(), TotalMembers: self.TotalMembers(), MemberLastFourSSN: self.MemberLastFourSSN(), NoSSN: self.NoSSN() };
            var jqxhr = $.post("/Home/SaveStep3", postData,
                function (data) { })
              .always(function () {
                  self.GoToStep(self.StepForward(3));
              });
        }
    };

    self.step4Complete = function () {
        if (self.IsStep4Valid()) {
            var postData = { AppId: self.Id(), StreetAddress: self.StreetAddress(), AptNo: self.AptNo(), City: self.City(), State: self.State(), Zip: self.Zip(), Phone: self.Phone(), Email: self.Email(), AdultFilledByName: self.AdultFilledByName(), EthnicityId: self.EthnicityId(), Races: self.Races(), StepsRequired: self.StepsRequired() };
            var jqxhr = $.post("/Home/SaveContactInformation", postData, function (data) {
            }).always(function () {
                window.location.replace("/Home/Confirmation/" + self.Id());
            });
        }
    };

    self.step2Back = function () {
        self.GoToStep(self.StepBack(2));
    };

    self.step3Back = function () {
        self.GoToStep(self.StepBack(3));
    };

    self.step4Back = function () {
        self.GoToStep(self.StepBack(4));
    };

    self.IsStep1Valid = ko.computed(function () {
        return self.Children().length > 0 && !self.childFormEditMode();
    }, this);

    self.IsStep2Valid = ko.computed(function () {
        var IsValid = true;
        if (self.AssistanceProgramId() == null) {
            IsValid = false;
        }
        else if (self.AssistanceProgramId() != self.AssistanceProgramNoneId() && (self.AssistanceProgramCaseNumber() == null || self.AssistanceProgramCaseNumber() == "")) {
            IsValid = false;
        }
        return IsValid;
    }, this);

    self.IsStep3Valid = function () {
        self.IsIncomeComplete();
        var IsValid = true;
        var HasOtherErrors = false;
        if (!self.NoSSN()) {
            var ssnInvalid = self.ssnRequiredAndIsInvalid();
            IsValid = ssnInvalid;
            if (!ssnInvalid) {
                HasOtherErrors = true;
            }
        }
        if (self.Adults().length == 0) {
            IsValid = false;
            self.StepErrors.push('Please add atleast 1  Adult Member.');
        }
        if (!self.IsTotalMembersValid()) {
            IsValid = false;
            HasOtherErrors = true;
        }

        if (!self.IsIncomeComplete(true)) {
            IsValid = false;
            self.StepErrors.push('Please enter income for all Children.');
        }

        if (!self.IsIncomeComplete(false)) {
            IsValid = false;
            self.StepErrors.push('Please enter income for all Adults.');
        }

        if (HasOtherErrors) {
            self.StepErrors.push('Please fix errors below.');
        }
        if (!IsValid) {
            self.StepErrorsShow();
        }
        return IsValid;
    };

    self.IsStep4Valid = function () {
        var IsValid = true;
        if (!self.IsCityValid()) { IsValid = false; }
        if (!self.IsStateValid()) { IsValid = false; }
        if (!self.IsZipValid()) { IsValid = false; }
        if (!self.IsAdultFilledByNameValid()) { IsValid = false; }
        if (!IsValid) {
            self.StepErrors.push('Please fix errors below.');
            self.StepErrorsShow();
        }
        return IsValid;
    };

    self.GoToStep = function (val) {
        $(".div-Steps").hide();
        $("#divStep" + val).slideDown("slow", function () { });
    };

    self.StepForward = function (currStep) {
        var nextStep = 0;
        var found = false;
        ko.utils.arrayForEach(self.StepsRequired(), function (step) {
            if (step > currStep && !found) {
                nextStep = step;
                found = true;
            }
        });
        return nextStep;
    };

    self.StepsRequired = function () {
        var steps = [];
        steps.push(1);

        //Step 2 logic
        var countHMR = 0;
        var countFoster = 0;
        ko.utils.arrayForEach(self.Children(), function (child) {
            if (child.IsHMR()) {
                ++countHMR;
            }
            if (child.IsFoster()) {
                ++countFoster;
            }
        });
        if (countFoster == self.Children().length && countHMR == 0) {
            //console.log('do not add step 2');
        }
        else {
            steps.push(2);

            //Step logic 3 -- if user did not enter any assistance program then he completes income section
            if (self.AssistanceProgramIsRequired()) {
                //console.log('skip 3'); 
            }
            else { steps.push(3); }
        }

        steps.push(4);
        //console.log(steps);                    
        return steps;
    };

    //self.IsStep2Required = function () {        
    //    var isRequired = $.inArray(2, self.StepsRequired()) >= 0;
    //    return isRequired;
    //};

    //self.IsStep3Required = function () {
    //    var isRequired = $.inArray(3, self.StepsRequired()) >= 0;
    //    return isRequired;
    //};

    self.StepBack = function (currStep) {
        var backStep = 0;
        var found = false;
        ko.utils.arrayForEach(self.StepsRequired(), function (step) {
            if (step >= currStep) {
                found = true;
            }
            if (!found) {
                backStep = step;
            }
        });
        return backStep;
    };

    self.StepErrorsClear = function () {
        $(".alert").hide();
        self.StepErrors.removeAll();
    };

    self.StepErrorsShow = function () {
        $(".alert").fadeTo(4000, 500).slideUp(500, function () {
            self.StepErrorsClear();
        });
    };

    self.IncomeStepErrorsClear = function () {
        $("#IncomeAlertDiv").hide();
        $("#IncomeAlertDiv").removeClass("alert");
        self.StepErrors.removeAll();
    };

    self.IncomeStepErrorsShow = function () {
        $("#IncomeAlertDiv").addClass("alert");
        $("#IncomeAlertDiv").fadeTo(4000, 500).slideUp(500, function () {
            self.StepErrorsClear();
        });
    };

    ko.bindingHandlers.numeric = {
        init: function (element, valueAccessor) {
            $(element).on("keydown", function (event) {
                // Allow: backspace, delete, tab, escape, and enter
                if (event.keyCode == 46 || event.keyCode == 8 || event.keyCode == 9 || event.keyCode == 27 || event.keyCode == 13 ||
                    // Allow: Ctrl+A
                    (event.keyCode == 65 && event.ctrlKey === true) ||
                    // Allow: . ,
                    (event.keyCode == 188 || event.keyCode == 190 || event.keyCode == 110) ||
                    // Allow: home, end, left, right
                    (event.keyCode >= 35 && event.keyCode <= 39)) {
                    // let it happen, don't do anything
                    return;
                }
                else {
                    // Ensure that it is a number and stop the keypress
                    if (event.shiftKey || (event.keyCode < 48 || event.keyCode > 57) && (event.keyCode < 96 || event.keyCode > 105)) {
                        event.preventDefault();
                    }
                }
            });
        }
    };

    self.MemberIncome = function (member) {
        $.getJSON('/Home/MemberIncome/' + member.Id(), function (json) {
            ko.mapping.fromJS(json, {}, self.ModalMemberIncome);
            //console.log(self.ModalMemberIncome);
            $("#incomeModal").modal("show");
        });
    };

    self.MemberIncomeSave = function () {
        if (self.MemberIncomeValidation())
        {  //SaveMemberIncome         
            $.ajax({
                method: "POST",
                url: "/Home/SaveMemberIncome",
                data: ko.toJSON(self.ModalMemberIncome),
                dataType: "json",
                contentType: "application/json; charset=utf-8"
            }).done(function (members) {
                
                if (self.ModalMemberIncome.IsChild())
                {
                    ko.mapping.fromJS(members, {}, self.Children);
                }
                else
                {
                    ko.mapping.fromJS(members, {}, self.Adults);
                }
                $("#incomeModal").modal("hide");
            });
        }
    };       

    self.MemberIncomeValidation = function () {
        //console.log(self.StepErrors());
        var IsValid = true;
        var responseRequiredErrorAdded = false;
        //console.log(self.ModalMemberIncome.IncomeResponses());
        ko.utils.arrayForEach(self.ModalMemberIncome.IncomeResponses(), function (response) {
            if (response.Response() == null) //user has not responded to yes/no
            {
                IsValid = false;
                response.ResponseError("Response Required.");
            }
            else {
                response.ResponseError("");
            }
        });
        if (!IsValid) {
            self.StepErrors.push('Please respond Yes or No to all Questions.');
        }

        if (IsValid) {
            IsValid = self.MemberIncomeValidateQuestionResponses();
        }

        if (!IsValid) {
            self.IncomeStepErrorsShow();
        }
        console.log(IsValid);
        return IsValid;
    };

    self.MemberIncomeValidateQuestionResponses = function () {
        var IsValid = true;
        var responseRequiredErrorAdded = false;
        var responseRequiredDetailErrorAdded = false;
        ko.utils.arrayForEach(self.ModalMemberIncome.IncomeResponses(), function (response) {           
            if (response.Response()) 
            {
                //user answered yes to income, 
                //1. check if user provided atleast one amount and frequency
                //2. for all detail records , make sure both amount and frequence are selected
                if (response.ResponseDetails().length == 0) {
                    response.ResponseError("Please Add Income Records.");
                    if (!responseRequiredErrorAdded) {
                        self.StepErrors.push('Please add Income Records for all questions to which you have responded Yes.');
                    }
                    responseRequiredErrorAdded = true;
                    IsValid = false;
                }
                else {
                    response.ResponseError("");
                }
                
                ko.utils.arrayForEach(response.ResponseDetails(), function (detail) {
                    //console.log(detail);
                    var IsDetailValid = self.IsIncomeDetailValid(detail);
                    if (!IsDetailValid)
                    {
                        IsValid = false;
                        if (!responseRequiredDetailErrorAdded)
                        {
                            self.StepErrors.push('For Income records, both Amount and Frequency are required.');
                        }
                        responseRequiredDetailErrorAdded = true;
                    }
                })
            }
        });
        return IsValid;
    };

    self.IsIncomeDetailValid = function (detail)
    {
        var IsValid = true;
        if (detail.Amount == "" || detail.FrequencyId == "" || !detail.FrequencyId) {
            detail.DetailError("Both Amount and Frequency are required.");
            IsValid = false;
        } else {
            detail.DetailError("");
        }
        return IsValid;
    }

    self.IncomeQuestionResponseChanged = function (question) {
        if (question.Response() == true || question.Response() == false) //user has responded to yes/no
        {
            question.ResponseError(""); //clear any errors
        }
    };

    self.MemberIncomeAdd = function (question) {
        //console.log(question);
        question.ResponseDetails.push({ Amount: "", FrequencyId: "", DetailError: ko.observable("") });
    };

    self.MemberIncomeRemove = function (income) {
        $.each(self.ModalMemberIncome.IncomeResponses(), function () { this.ResponseDetails.remove(income) });        
    };

}