var course = {};

$('#courseForm').submit(function (e) {
    e.preventDefault();

    postCourse();
});


function postCourse() {
    course.Name = $('#courseName').val();
    course.Type = $('#courseType').val();

    loadingBtn('btnSubmit', true);

    $.ajax({
        type: 'POST',
        url: '/api/courses',
        data: { schoolId: 1, model: course },
        dataType: 'json',
        error: function (response) {

            if (response.status === 200) {
                loadingBtn('btnSubmit', false);
                yay(response.responseText);
                setTimeout(function () {
                    location.reload();
                }, 700);
            } else {
                loadingBtn('btnSubmit', false);
                error(response.responseText);
            }
        }
    });
}

function onDeleteCourse(id) {
    $.confirm({
        theme: 'supervan',
        title: 'Delete this record?',
        content: 'Are you sure you want to delete this course?',
        type: 'red',
        typeAnimated: true,
        buttons: {
            yes: {
                text: 'Delete',
                btnClass: 'btn-red',
                action: function () {
                    deleteCourse(id);
                }
            },
            No: function () {
                
            }
        }
    })
};

function deleteCourse(id) {
    $.confirm({
        theme: 'supervan',
        content: function () {
            var self = this;

            return $.ajax({
                url: '/api/courses/'+id,
                dataType: 'json',
                method: 'DELETE',
            }).fail(function (response) {
                if (response.status === 200) {
                    self.setType('green');
                    self.setIcon('fa fa-check-circle');
                    self.setTitle(response.responseText);
                    self.setContent('Record deleted successfully.');
                    self.onAction = function () {
                        setTimeout(function () {
                            location.reload();
                        }, 500);
                    }
                    
                } else {
                    self.setType('red');
                    self.setIcon('fa fa-ban');
                    self.setContent(response.responseText);
                    self.setTitle(response.statusText);
                }
            })
        }
    })
}

function onSearch() {

}
var btn_text = '';

function loadingBtn(id, bool) {
    $('#' + id).prop('disabled', bool);

    if (bool) {
        btn_text = $('#' + id).text();

        $('#' + id).html("<i class='fa fa-circle-o-notch fa-spin'></i> posting...");
    } else {
        $('#' + id).text(btn_text);
    }
}

function yay(msg) {
    toastr.success(msg);
}

function error(msg) {
    toastr.error(msg);
}

function deleteConfirm() {
    
}

function disableLink(e) {
    e.preventDefault();

    $(this).prop('disabled', true);
}
var unit = {};

$('#unitForm').submit(function (e) {
    e.preventDefault();

    postUnit();
})

$('#unitBtnSubmit').click(function (e) {
    e.preventDefault();

    postUnit();
})

function postUnit() {
    unit.Name = $('#unitName').val();

    loadingBtn('unitBtnSubmit', true);

    $.ajax({
        type: 'POST',
        url: '/api/units',
        data: {
            courseId: $('#CourseId').val(),
            model: unit
        },
        dataType: 'json',
        error: function (response) {
            console.log(response);
            if (response.status === 200 || response.status === 201) {
                loadingBtn('unitBtnSubmit', false);
                yay(response.responseText);
                setTimeout(function () {
                    location.reload();
                }, 700);
            } else {
                loadingBtn('unitBtnSubmit', false);

                if (response.responseText) {
                    error(response.responseText);
                } else {
                    error(response.statusText);
                }
                
            }
        }
    });
}