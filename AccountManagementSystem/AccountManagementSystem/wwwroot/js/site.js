// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


$(document).ready(function () {
    $(".navbar .nav-item.dropdown a.multi-dropdown").on('click', function (e) {
        e.stopPropagation();
    });

    var findLink = $('a[href="' + location.pathname + '"]');
    findLink.addClass('active');
    findLink.closest('li.nav-item').find('.nav-link').addClass('active');
});

document.addEventListener('DOMContentLoaded', function () {
    // Select all dropdown list items
    const dropdowns = document.querySelectorAll('.navbar-nav .dropdown');
    const mediaQuery = window.matchMedia('(min-width: 992px)'); // Matches Bootstrap's 'lg' breakpoint

    function applyHoverBehavior() {
        if (mediaQuery.matches) {
            // Desktop view: Apply hover
            dropdowns.forEach(function (dropdown) {
                dropdown.addEventListener('mouseenter', function () {
                    const dropdownMenu = this.querySelector('.dropdown-menu');
                    if (dropdownMenu) {
                        this.classList.add('show'); // Add 'show' to the li parent
                        dropdownMenu.classList.add('show'); // Add 'show' to the dropdown menu
                        dropdownMenu.setAttribute('data-bs-popper', 'static'); // Important for correct positioning on hover
                    }
                });

                dropdown.addEventListener('mouseleave', function () {
                    const dropdownMenu = this.querySelector('.dropdown-menu');
                    if (dropdownMenu) {
                        this.classList.remove('show'); // Remove 'show' from the li parent
                        dropdownMenu.classList.remove('show'); // Remove 'show' from the dropdown menu
                        dropdownMenu.removeAttribute('data-bs-popper'); // Remove attribute
                    }
                });

                // Ensure the dropdown toggle link does not trigger Bootstrap's click behavior
                const dropdownToggle = dropdown.querySelector('.dropdown-toggle');
                if (dropdownToggle && dropdownToggle.hasAttribute('data-bs-toggle')) {
                    dropdownToggle.removeAttribute('data-bs-toggle');
                }
            });
        } else {
            // Mobile view: Revert to Bootstrap's default click behavior
            dropdowns.forEach(function (dropdown) {
                // Remove event listeners for hover
                dropdown.removeEventListener('mouseenter', function () { }); // Placeholder for removal, actual removal needs named function
                dropdown.removeEventListener('mouseleave', function () { }); // Placeholder for removal, actual removal needs named function

                // Re-add data-bs-toggle for mobile click behavior
                const dropdownToggle = dropdown.querySelector('.dropdown-toggle');
                if (dropdownToggle && !dropdownToggle.hasAttribute('data-bs-toggle')) {
                    dropdownToggle.setAttribute('data-bs-toggle', 'dropdown');
                }

                // Remove 'show' classes that might have been left from desktop hover
                const dropdownMenu = dropdown.querySelector('.dropdown-menu');
                if (dropdownMenu) {
                    dropdown.classList.remove('show');
                    dropdownMenu.classList.remove('show');
                    dropdownMenu.removeAttribute('data-bs-popper');
                }
            });
        }
    }

    // Initial application of behavior
    applyHoverBehavior();

    // Listen for window resize to adjust behavior for responsiveness
    mediaQuery.addEventListener('change', applyHoverBehavior);

});

function bootstrapNotify(type) {
    var notifyOption = {
        type: type,
        animate: {
            enter: 'animated fadeInDown'
        },
        placement: {
            from: "top",
            align: "center"
        },
        spacing: 10,
        z_index: 1031,
        delay: 1000,
        template: '' +
            '<div data-notify="container" class="alert alert-dismissible alert-{0} alert-notify" role="alert">' +
            '<span class="alert-icon" data-notify="icon"></span>' +

            '<div class="alert-text"</div>' +
            '<span class="alert-title" data-notify="title">{1}</span>' + '<span class="small" data-notify="message">{2}</span>' +
            '</div>' +

            '<button type="button" class="close" data-notify="dismiss" aria-label="Close">' +
            '<span aria-hidden="true">&times;</span>' +
            '</button>' +
            '</div>'
    };
    return notifyOption;
}

function successNotify(message = "Successfully Saved!", pageReload = false) {
    $.notify({
        title: 'Success!',
        message: message,
        html: true,
    }, bootstrapNotify(type = "success"));

    if (pageReload) {
        location.reload();
    }
}

function errorNotify(message = "Something went wrong!", pageReload = false) {
    $.notify({
        title: 'Alert!',
        message: message,
        html: true,
    }, bootstrapNotify(type = "warning"));

    if (pageReload) {
        location.reload();
    }
}
