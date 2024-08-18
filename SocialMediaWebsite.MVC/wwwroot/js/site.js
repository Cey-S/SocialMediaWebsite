// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function RenderPostComponent(data) {
    $.each(data, function (index, post) {
        if ($("#" + post.PostId).length) {
            // If this post is already displayed on the page, skip to rendering next.
            // Temporary fix for the popular posts tab. Simultaneous interactions break the order.
            return true; 
        }
        $("#container").append(
            '<div id="' + post.PostId + '" class="card text-break border-0 shadow mb-5 ms-auto me-auto" style="max-width: 880px;">' +
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
                        '<a class="filled-heart" href="#" onclick="UnlikePost(' + post.PostId + ');return false;"><i class="fa-solid fa-heart fa-xl" style="color: #dc143c;"></i></a>' +
                        '<a class="empty-heart" href="#" onclick="LikePost(' + post.PostId + ');return false;"><i class="fa-regular fa-heart fa-xl" style="color: #000000;"></i></a>' +
                        '<span class="ms-1 fw-bold">' + post.totalLikes + '</span>' +
                    '</span>' +
                    // Comment
                    '<span id="' + post.PostId + 'commentButton" class="card-link">' +
                        '<a data-bs-toggle="collapse" href="#collapsable' + post.PostId + '"><i class="fa-regular fa-comment fa-xl" style="color: #000000;"></i></a>' +
                        '<span class="ms-1 fw-bold">' + post.totalComments + '</span>' +
                    '</span>' +
                    // Repost
                    '<span id="' + post.PostId + 'repostButton" class="card-link">' +
                        '<a href="#" onclick="Repost(' + post.PostId + ');return false;"><i class="fa-solid fa-retweet fa-xl" style="color: #000000;"></i></a>' +
                        '<span class="ms-1 fw-bold">' + post.totalReposts + '</span>' +
                        '<input type="hidden" value="' + post.isReposted + '" />' +
                    '</span>' + 
                    // Create Date
                    '<span class="float-end text-secondary">' + post.CreateDate + '</span>' +
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
                            '<div class="d-flex align-items-start justify-content-between gap-3">'+
                                '<div class="card-title d-flex align-items-center">' +
                                    '<a href="/Account/Profile?username=' + commentData.username + '" class="profile-link"><img src="' + commentData.imagePath + '" class="profile-picture rounded-circle" style="max-width: 30px; max-height:30px"/></a>' +
                                    '<a href="/Account/Profile?username=' + commentData.username + '" class="profile-link"><h6 class="card-subtitle ms-2">' + commentData.username + '</h6></a>' +
                                '</div>' +
                                '<span class="text-secondary">' + commentData.CreateDate + '</span>' +
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

        // If the signed-in user reposted the post before
        if (post.isReposted) {
            $("#" + post.PostId + "repostButton > a > svg").attr("fill", "#00BFFF");
        } else {
            $("#" + post.PostId + "repostButton > a > svg").attr("fill", "black");
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
                '<div class="d-flex align-items-start justify-content-between gap-3">' +
                '<div class="card-title d-flex align-items-center">' +
                '<a href="/Account/Profile?username=' + data.username + '" class="profile-link"><img src="' + data.imagePath + '" class="profile-picture rounded-circle" style="max-width: 30px; max-height:30px"/></a>' +
                '<a href="/Account/Profile?username=' + data.username + '" class="profile-link"><h6 class="card-subtitle ms-2">' + data.username + '</h6></a>' +
                '</div>' +
                '<span class="text-secondary">' + data.createTime + '</span>' +
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

function Repost(postId) {
    let totalReposts = $("#" + postId + "repostButton span").html();
    $.ajax({
        type: 'GET',
        url: '/api/Interaction/Repost',
        data: {
            "id": postId, "reposts": totalReposts, "isReposted": $("#" + postId + "repostButton input").val() },
        dataType: 'json',
        success: function (data) {
            if (data.isReposted) {
                $("#" + data.id + "repostButton svg").attr("fill", "DeepSkyBlue");
            } else {
                $("#" + data.id + "repostButton svg").attr("fill", "Black");
            }
            $("#" + postId + "repostButton input").val(data.isReposted);
            $("#" + data.id + "repostButton span").html(data.newTotal);
        }
    });
}
