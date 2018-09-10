$(document).ready(function () {

    $.validator.addMethod("verifyDate",
        function (value, element) {
            // yyyy-mm-dd
            var re = /^\d{4}-\d{1,2}-\d{1,2}$/;

            return (re.test(value));
        });
    $("#accountForm").validate({
        rules: {
            "userName": {
                required: true,
                minlength: 4
            },
            "employeeFirstName": "required",
            "employeeLastName": "required",
            "employeeDOB": {
                required: true,
                minlength: 8,
                verifyDate: true
            }
        },
        messages: {
            "userName": {
                required: "Username is a required field",
                minlength: "Username must be at least 4 characters long"
            },
            "employeeFirstName": { required: "Please enter first name" },
            "employeeLastName": { required: "Please enter last name" },
            "employeeDOB": {
                required: "Please enter a DOB",
                minlength: "Please check your DOB, you might be missing characters",
                verifyDate: "Date must be in the format of (YYYY-MM-DD)"
            }
        }

    });
});