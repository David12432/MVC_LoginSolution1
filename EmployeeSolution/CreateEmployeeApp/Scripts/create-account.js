$(document).ready(function () {

    $.validator.addMethod("verifyDate",
        function (value, element) {
            // yyyy-mm-dd
            var re = /^\d{4}-\d{1,2}-\d{1,2}$/;

            return (re.test(value));
        });
    $("#accountForm").validate({
        rules: {
            "User.Username": {
                required: true,
                minlength: 4
            },
            "Password": {
                required: true,
                minlength: 6,
                maxlength: 20
            },
            "ConfirmPassword": {
                required: true,
                minlength: 6,
                equalTo: "#Password"
            },
            "Employee.FirstName": "required",
            "Employee.LastName": "required",
            "Employee.DOB": {
                required: true,
                minlength: 8,
                verifyDate: true
            }
        },
        messages: {
            "User.Username": {
                required: "Username is a required field",
                minlength: "Username must be at least 4 characters long"
            },
            "Password": {
                required: "Password is a required field",
                minlength: "Password must be 6 characters long"
            },
            "ConfirmPassword": {
                required: "Please confirm password",
                minlength: "Password must be at least 6 characters long",
                maxlength: "Password can be no longer than 20 characters long",
                //equalTo: "Please verify passwords match"
            },
            "Employee.FirstName": { required: "Please enter first name" },
            "Employee.LastName": { required: "Please enter last name" },
            "Employee.DOB": {
                required: "Please enter a DOB",
                minlength: "Please check your DOB, you might be missing characters",
                verifyDate: "Date must be in the format of (YYYY-MM-DD)"
            }
        }

    });
});