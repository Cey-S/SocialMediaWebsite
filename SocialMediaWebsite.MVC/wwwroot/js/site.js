﻿// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function RenderPostComponent(data) {
    $.each(data, function (index, post) {
        $("#container").append(
            '<div id="' + post.PostId + '" class="card text-break border-0 shadow mb-5" style="max-width: 800px; ">' +
                // Card Header: Profile Picture, Username
                '<div class="card-header">' +
                    '<div class="d-flex align-items-center">' +
                        '<a href="/Account/Profile?username=' + post.Username + '" class="profile-link"><img src="' + post.ImagePath + '" class="profile-picture rounded-circle"/></a>' +
                        '<a href="/Account/Profile?username=' + post.Username + '" class="profile-link"><h5 class="card-subtitle ms-2">' + post.Username + '</h5></a>' +
                    '</div>' +
                '</div>' +
                // Card Body: Title, Body, Tags
                '<div class="card-body">' +
                    '<h4 class="card-title">' + post.Title + '</h4>' +
                    '<p class="card-text" style="white-space: pre-wrap">' + post.Body + '</p>' +
                    '<div id="' + post.PostId + 'tags"></div>' + // TAGS will be added here
                '</div>' +
                // Card Footer: Like, Comment, Share Buttons
                '<div class="card-footer">' +
                    // Like
                    '<span id="' + post.PostId + 'likeButtons" class="card-link">' +
                        '<a class="filled-heart" href="#" onclick="UnlikePost(' + post.PostId + ');return false;"><svg style="max-width: 23px" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 512 512"><!--!Font Awesome Free 6.6.0 by fontawesome - https://fontawesome.com License - https://fontawesome.com/license/free Copyright 2024 Fonticons, Inc.--><path fill="#DC143C" d="M47.6 300.4L228.3 469.1c7.5 7 17.4 10.9 27.7 10.9s20.2-3.9 27.7-10.9L464.4 300.4c30.4-28.3 47.6-68 47.6-109.5v-5.8c0-69.9-50.5-129.5-119.4-141C347 36.5 300.6 51.4 268 84L256 96 244 84c-32.6-32.6-79-47.5-124.6-39.9C50.5 55.6 0 115.2 0 185.1v5.8c0 41.5 17.2 81.2 47.6 109.5z"/></svg></a>' +
                        '<a class="empty-heart" href="#" onclick="LikePost(' + post.PostId + ');return false;"><svg style="max-width: 23px" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 512 512"><!--!Font Awesome Free 6.6.0 by fontawesome - https://fontawesome.com License - https://fontawesome.com/license/free Copyright 2024 Fonticons, Inc.--><path d="M225.8 468.2l-2.5-2.3L48.1 303.2C17.4 274.7 0 234.7 0 192.8l0-3.3c0-70.4 50-130.8 119.2-144C158.6 37.9 198.9 47 231 69.6c9 6.4 17.4 13.8 25 22.3c4.2-4.8 8.7-9.2 13.5-13.3c3.7-3.2 7.5-6.2 11.5-9c0 0 0 0 0 0C313.1 47 353.4 37.9 392.8 45.4C462 58.6 512 119.1 512 189.5l0 3.3c0 41.9-17.4 81.9-48.1 110.4L288.7 465.9l-2.5 2.3c-8.2 7.6-19 11.9-30.2 11.9s-22-4.2-30.2-11.9zM239.1 145c-.4-.3-.7-.7-1-1.1l-17.8-20-.1-.1s0 0 0 0c-23.1-25.9-58-37.7-92-31.2C81.6 101.5 48 142.1 48 189.5l0 3.3c0 28.5 11.9 55.8 32.8 75.2L256 430.7 431.2 268c20.9-19.4 32.8-46.7 32.8-75.2l0-3.3c0-47.3-33.6-88-80.1-96.9c-34-6.5-69 5.4-92 31.2c0 0 0 0-.1 .1s0 0-.1 .1l-17.8 20c-.3 .4-.7 .7-1 1.1c-4.5 4.5-10.6 7-16.9 7s-12.4-2.5-16.9-7z"/></svg></a>' +
                        '<span class="ms-1 fw-bold">' + post.totalLikes + '</span>' +
                    '</span>' +
                    // Comment
                    '<span id="' + post.PostId + 'commentButton" class="card-link">' +
                        '<a data-bs-toggle="collapse" href="#collapsable' + post.PostId + '"><svg style="max-width: 23px" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 512 512"><!--!Font Awesome Free 6.6.0 by fontawesome - https://fontawesome.com License - https://fontawesome.com/license/free Copyright 2024 Fonticons, Inc.--><path d="M123.6 391.3c12.9-9.4 29.6-11.8 44.6-6.4c26.5 9.6 56.2 15.1 87.8 15.1c124.7 0 208-80.5 208-160s-83.3-160-208-160S48 160.5 48 240c0 32 12.4 62.8 35.7 89.2c8.6 9.7 12.8 22.5 11.8 35.5c-1.4 18.1-5.7 34.7-11.3 49.4c17-7.9 31.1-16.7 39.4-22.7zM21.2 431.9c1.8-2.7 3.5-5.4 5.1-8.1c10-16.6 19.5-38.4 21.4-62.9C17.7 326.8 0 285.1 0 240C0 125.1 114.6 32 256 32s256 93.1 256 208s-114.6 208-256 208c-37.1 0-72.3-6.4-104.1-17.9c-11.9 8.7-31.3 20.6-54.3 30.6c-15.1 6.6-32.3 12.6-50.1 16.1c-.8 .2-1.6 .3-2.4 .5c-4.4 .8-8.7 1.5-13.2 1.9c-.2 0-.5 .1-.7 .1c-5.1 .5-10.2 .8-15.3 .8c-6.5 0-12.3-3.9-14.8-9.9c-2.5-6-1.1-12.8 3.4-17.4c4.1-4.2 7.8-8.7 11.3-13.5c1.7-2.3 3.3-4.6 4.8-6.9l.3-.5z"/></svg></a>' +
                        '<span class="ms-1 fw-bold">' + post.totalComments + '</span>' +
                    '</span>' +
                    // Share
                    '<span class="card-link">' +
                        '<a class="card-link" href="#"><svg style="max-width: 23px" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 576 512"><!--!Font Awesome Free 6.6.0 by fontawesome - https://fontawesome.com License - https://fontawesome.com/license/free Copyright 2024 Fonticons, Inc.--><path d="M272 416c17.7 0 32-14.3 32-32s-14.3-32-32-32l-112 0c-17.7 0-32-14.3-32-32l0-128 32 0c12.9 0 24.6-7.8 29.6-19.8s2.2-25.7-6.9-34.9l-64-64c-12.5-12.5-32.8-12.5-45.3 0l-64 64c-9.2 9.2-11.9 22.9-6.9 34.9s16.6 19.8 29.6 19.8l32 0 0 128c0 53 43 96 96 96l112 0zM304 96c-17.7 0-32 14.3-32 32s14.3 32 32 32l112 0c17.7 0 32 14.3 32 32l0 128-32 0c-12.9 0-24.6 7.8-29.6 19.8s-2.2 25.7 6.9 34.9l64 64c12.5 12.5 32.8 12.5 45.3 0l64-64c9.2-9.2 11.9-22.9 6.9-34.9s-16.6-19.8-29.6-19.8l-32 0 0-128c0-53-43-96-96-96L304 96z"/></svg></a>' +
                        '<span class="ms-1 fw-bold">0</span>' +
                    '</span>' +
                '</div>' +
                // Collapsable Comments: Comment writing textarea, User Comments
                '<div class="collapse" id="collapsable' + post.PostId + '">' +
                    // Comment area: Text Area, Send Button
                    '<div class="border-top border-bottom card-body">' +
                        '<div class="row">' +
                            // Text area
                            '<div class="col">' +
                                '<div class="form-floating">' +
                                    '<textarea class="form-control" placeholder="Leave a comment here" id="commentTextArea' + post.PostId + '" maxlength="1000" oninput="this.style.height=&#39;&#39;;this.style.height = this.scrollHeight + &#39;px&#39;"></textarea>' +
                                    '<label for="commentTextArea' + post.PostId + '">Comment</label>' +
                                '</div>' +
                            '</div>' +
                            // Send Button
                            '<div class="col-1 d-flex align-items-end" style="min-width: 50px">' +
                                '<a class="w-100 pb-2" href="#" onclick="SendComment(' + post.PostId + ');return false;"><svg style="max-width: 23px" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 512 512"><!--!Font Awesome Free 6.6.0 by fontawesome - https://fontawesome.com License - https://fontawesome.com/license/free Copyright 2024 Fonticons, Inc.--><path d="M498.1 5.6c10.1 7 15.4 19.1 13.5 31.2l-64 416c-1.5 9.7-7.4 18.2-16 23s-18.9 5.4-28 1.6L284 427.7l-68.5 74.1c-8.9 9.7-22.9 12.9-35.2 8.1S160 493.2 160 480l0-83.6c0-4 1.5-7.8 4.2-10.8L331.8 202.8c5.8-6.3 5.6-16-.4-22s-15.7-6.4-22-.7L106 360.8 17.7 316.6C7.1 311.3 .3 300.7 0 288.9s5.9-22.8 16.1-28.7l448-256c10.7-6.1 23.9-5.5 34 1.4z"/></svg></a>' +
                            '</div>' +
                        '</div>' +
                    '</div>' +
                    // User Comments
                    '<div id="' + post.PostId + 'comments" class="w-100 ps-5 pe-5 d-flex flex-column align-items-start">' +
                    // COMMENTS will be added here
                    '</div>' +
                '</div>' +
            '</div>'
        );

        // Add comments if any
        if (post.totalComments > 0) {
            $("#" + post.PostId + "comments").addClass("pt-3");
            $.each(post.Comments, function (i, commentData) {
                $("#" + post.PostId + "comments").append(
                    '<div class="card text-break mb-3">' +
                        '<div class="card-body bg-light">' +
                            '<div class="card-title d-flex align-items-center">' +
                                '<a href="/Account/Profile?username=' + commentData.username + '" class="profile-link"><img src="' + commentData.imagePath + '" class="profile-picture rounded-circle" style="max-width: 30px; max-height:30px"/></a>' +
                                '<a href="/Account/Profile?username=' + commentData.username + '" class="profile-link"><h6 class="card-subtitle ms-2">' + commentData.username + '</h6></a>' +
                            '</div>' +
                            '<p class="card-text" style="white-space: pre-wrap">' + commentData.content + '</p>' +
                        '</div>' +
                    '</div>'
                );
            });
        }

        // If the signed-in user liked the post before, then hide the empty heart icon
        if (post.isLiked) {
            $("#" + post.PostId + "likeButtons .empty-heart").addClass("d-none");
        } else {
            $("#" + post.PostId + "likeButtons .filled-heart").addClass("d-none");
        }

        // Add tags if any
        var footerId = "#" + post.PostId + "tags";
        $.each(post.PostTags, function (j, tag) {
            $(footerId).append('<a class="me-2 text-nowrap text-decoration-none text-secondary" href="/Post/TagSearch?name=' + tag + '">#' + tag + '</a>');
        });
    });
}

function LikePost(postId) {
    let totalLikes = $("#" + postId + "likeButtons span").html();
    $.ajax({
        type: 'GET',
        url: '/api/Interaction/Like',
        data: { "id": postId, "likes": totalLikes },
        dataType: 'json',
        success: function (data) {
            $("#" + data.id + "likeButtons .filled-heart").removeClass("d-none");
            $("#" + data.id + "likeButtons .empty-heart").addClass("d-none");
            $("#" + data.id + "likeButtons span").html(data.newTotal);
        }
    });
}

function UnlikePost(postId) {
    let totalLikes = $("#" + postId + "likeButtons span").html();
    $.ajax({
        type: 'GET',
        url: '/api/Interaction/Unlike',
        data: { "id": postId, "likes": totalLikes },
        dataType: 'json',
        success: function (data) {
            $("#" + data.id + "likeButtons .empty-heart").removeClass("d-none");
            $("#" + data.id + "likeButtons .filled-heart").addClass("d-none");
            $("#" + data.id + "likeButtons span").html(data.newTotal);
        }
    });
}

function SendComment(postId) {
    let totalComments = $("#" + postId + "commentButton span").html();
    let comment = $("#commentTextArea" + postId).val();
    if (!comment) {
        return false;
    }
    $.ajax({
        type: 'GET',
        url: '/api/Interaction/SendComment',
        data: { "id": postId, "comments": totalComments, "content": comment },
        dataType: 'json',
        success: function (data) {
            if (data.newTotal == 1) {
                $("#" + data.id + "comments").addClass("pt-3");
            }
            $("#" + data.id + "comments").append(
                '<div class="card text-break mb-3">' +
                '<div class="card-body bg-light">' +
                '<div class="card-title d-flex align-items-center">' +
                '<a href="/Account/Profile?username=' + data.username + '" class="profile-link"><img src="' + data.imagePath + '" class="profile-picture rounded-circle" style="max-width: 30px; max-height:30px"/></a>' +
                '<a href="/Account/Profile?username=' + data.username + '" class="profile-link"><h6 class="card-subtitle ms-2">' + data.username + '</h6></a>' +
                '</div>' +
                '<p class="card-text" style="white-space: pre-wrap">' + data.content + '</p>' +
                '</div>' +
                '</div>'
            );
            $("#" + data.id + "commentButton span").html(data.newTotal);
            $("#commentTextArea" + data.id).val("").height("");
        }
    });
}
