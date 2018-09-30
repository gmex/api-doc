







<!DOCTYPE html>
<html lang="en">
  <head>
    <meta charset="utf-8">
  <link rel="dns-prefetch" href="https://assets-cdn.github.com">
  <link rel="dns-prefetch" href="https://avatars0.githubusercontent.com">
  <link rel="dns-prefetch" href="https://avatars1.githubusercontent.com">
  <link rel="dns-prefetch" href="https://avatars2.githubusercontent.com">
  <link rel="dns-prefetch" href="https://avatars3.githubusercontent.com">
  <link rel="dns-prefetch" href="https://github-cloud.s3.amazonaws.com">
  <link rel="dns-prefetch" href="https://user-images.githubusercontent.com/">



  <link crossorigin="anonymous" media="all" integrity="sha512-mjQPRAh2Y9A0sPdZzipNfPO7PT4g06mk0uZs15DbL/vsNCRGx1uRzWVzls9MJCoy2yRNjaMmEVFKJDpCui00mA==" rel="stylesheet" href="https://assets-cdn.github.com/assets/frameworks-df973073d880f28fbbae0263fb1ef62b.css" />
  <link crossorigin="anonymous" media="all" integrity="sha512-k4rXi2xAgpvXlB7r/tZ1ski3o3AWXfn7Z6hx6C/g9CcFeM5miuGB8zJFRgQW5wDKRaNQfv42R9F707X/2WqAQg==" rel="stylesheet" href="https://assets-cdn.github.com/assets/github-2b520d809bcf76c745c815d9523f0a00.css" />
  
  
  
  

  <meta name="viewport" content="width=device-width">
  
  <title>api-doc/WebSocket_API_for_GMEX_v1.md at master · testgmex/api-doc</title>
    <meta name="description" content="Gmex API docs for test! Contribute to testgmex/api-doc development by creating an account on GitHub.">
    <link rel="search" type="application/opensearchdescription+xml" href="/opensearch.xml" title="GitHub">
  <link rel="fluid-icon" href="https://github.com/fluidicon.png" title="GitHub">
  <meta property="fb:app_id" content="1401488693436528">

    
    <meta property="og:image" content="https://avatars3.githubusercontent.com/u/43609835?s=400&amp;v=4" /><meta property="og:site_name" content="GitHub" /><meta property="og:type" content="object" /><meta property="og:title" content="testgmex/api-doc" /><meta property="og:url" content="https://github.com/testgmex/api-doc" /><meta property="og:description" content="Gmex API docs for test! Contribute to testgmex/api-doc development by creating an account on GitHub." />

  <link rel="assets" href="https://assets-cdn.github.com/">
  <link rel="web-socket" href="wss://live.github.com/_sockets/VjI6MzIxNzEzMjg4OjZjZjExZmE1ODkxM2ZiODA3YzMyNTRmMTU2ZTI5OTg2YzI5MTM1YzMzOTBkYWQ0NmNkMTk0MmRkN2EzYTYzNjU=--f6bd471168f6e84915d28fa209eec3c4daa83ba3">
  <meta name="pjax-timeout" content="1000">
  <link rel="sudo-modal" href="/sessions/sudo_modal">
  <meta name="request-id" content="95D2:43C0:1F52CE4:306386B:5BB07C5A" data-pjax-transient>


  

  <meta name="selected-link" value="repo_source" data-pjax-transient>

      <meta name="google-site-verification" content="KT5gs8h0wvaagLKAVWq8bbeNwnZZK1r1XQysX3xurLU">
    <meta name="google-site-verification" content="ZzhVyEFwb7w3e0-uOTltm8Jsck2F5StVihD0exw2fsA">
    <meta name="google-site-verification" content="GXs5KoUUkNCoaAZn7wPN-t01Pywp9M3sEjnt_3_ZWPc">

  <meta name="octolytics-host" content="collector.githubapp.com" /><meta name="octolytics-app-id" content="github" /><meta name="octolytics-event-url" content="https://collector.githubapp.com/github-external/browser_event" /><meta name="octolytics-dimension-request_id" content="95D2:43C0:1F52CE4:306386B:5BB07C5A" /><meta name="octolytics-dimension-region_edge" content="iad" /><meta name="octolytics-dimension-region_render" content="iad" /><meta name="octolytics-actor-id" content="43609835" /><meta name="octolytics-actor-login" content="testgmex" /><meta name="octolytics-actor-hash" content="027175e466dbef977c5e25862e7e0b2bfce8fbba25ea1457cdb421eeed96a4e3" />
<meta name="analytics-location" content="/&lt;user-name&gt;/&lt;repo-name&gt;/blob/show" data-pjax-transient="true" />



    <meta name="google-analytics" content="UA-3769691-2">

  <meta class="js-ga-set" name="userId" content="fded01f369cc64e21c6f3ce15e48d90d" %>

<meta class="js-ga-set" name="dimension1" content="Logged In">



  

      <meta name="hostname" content="github.com">
    <meta name="user-login" content="testgmex">

      <meta name="expected-hostname" content="github.com">
    <meta name="js-proxy-site-detection-payload" content="YWY1ZWU2OGQ1MmY0MmRhZmUzMTYzYmU0YjIyYTZkNmVlNmRiOTZjNDA0MTlmYWVhOGZlNTAxZmMyOWJkNTA5YXx7InJlbW90ZV9hZGRyZXNzIjoiNDcuNTIuNjYuMTY2IiwicmVxdWVzdF9pZCI6Ijk1RDI6NDNDMDoxRjUyQ0U0OjMwNjM4NkI6NUJCMDdDNUEiLCJ0aW1lc3RhbXAiOjE1MzgyOTI4MzcsImhvc3QiOiJnaXRodWIuY29tIn0=">

    <meta name="enabled-features" content="DASHBOARD_V2_LAYOUT_OPT_IN,EXPLORE_DISCOVER_REPOSITORIES,UNIVERSE_BANNER,MARKETPLACE_PLAN_RESTRICTION_EDITOR,MARKETPLACE_RETARGETING,COLLAPSE_REPEATED_COMMENTS">

  <meta name="html-safe-nonce" content="dc06795ef72e18c4809c887713463a3293e21a95">

  <meta http-equiv="x-pjax-version" content="79a2296b1f91f35abb74b9ce5368d71b">
  

      <link href="https://github.com/testgmex/api-doc/commits/master.atom" rel="alternate" title="Recent Commits to api-doc:master" type="application/atom+xml">

  <meta name="go-import" content="github.com/testgmex/api-doc git https://github.com/testgmex/api-doc.git">

  <meta name="octolytics-dimension-user_id" content="43609835" /><meta name="octolytics-dimension-user_login" content="testgmex" /><meta name="octolytics-dimension-repository_id" content="150428711" /><meta name="octolytics-dimension-repository_nwo" content="testgmex/api-doc" /><meta name="octolytics-dimension-repository_public" content="true" /><meta name="octolytics-dimension-repository_is_fork" content="false" /><meta name="octolytics-dimension-repository_network_root_id" content="150428711" /><meta name="octolytics-dimension-repository_network_root_nwo" content="testgmex/api-doc" /><meta name="octolytics-dimension-repository_explore_github_marketplace_ci_cta_shown" content="true" />


    <link rel="canonical" href="https://github.com/testgmex/api-doc/blob/master/WebSocket_API_for_GMEX_v1.md" data-pjax-transient>


  <meta name="browser-stats-url" content="https://api.github.com/_private/browser/stats">

  <meta name="browser-errors-url" content="https://api.github.com/_private/browser/errors">

  <link rel="mask-icon" href="https://assets-cdn.github.com/pinned-octocat.svg" color="#000000">
  <link rel="icon" type="image/x-icon" class="js-site-favicon" href="https://assets-cdn.github.com/favicon.ico">

<meta name="theme-color" content="#1e2327">


  <meta name="u2f-support" content="true">

  <link rel="manifest" href="/manifest.json" crossOrigin="use-credentials">

  </head>

  <body class="logged-in env-production page-blob">
    

  <div class="position-relative js-header-wrapper ">
    <a href="#start-of-content" tabindex="1" class="p-3 bg-blue text-white show-on-focus js-skip-to-content">Skip to content</a>
    <div id="js-pjax-loader-bar" class="pjax-loader-bar"><div class="progress"></div></div>

    
    
    



        
<header class="Header  f5" role="banner">
  <div class="d-flex flex-justify-between px-3 ">
    <div class="d-flex flex-justify-between ">
      <div class="">
        <a class="header-logo-invertocat" href="https://github.com/" data-hotkey="g d" aria-label="Homepage" data-ga-click="Header, go to dashboard, icon:logo">
  <svg height="32" class="octicon octicon-mark-github" viewBox="0 0 16 16" version="1.1" width="32" aria-hidden="true"><path fill-rule="evenodd" d="M8 0C3.58 0 0 3.58 0 8c0 3.54 2.29 6.53 5.47 7.59.4.07.55-.17.55-.38 0-.19-.01-.82-.01-1.49-2.01.37-2.53-.49-2.69-.94-.09-.23-.48-.94-.82-1.13-.28-.15-.68-.52-.01-.53.63-.01 1.08.58 1.23.82.72 1.21 1.87.87 2.33.66.07-.52.28-.87.51-1.07-1.78-.2-3.64-.89-3.64-3.95 0-.87.31-1.59.82-2.15-.08-.2-.36-1.02.08-2.12 0 0 .67-.21 2.2.82.64-.18 1.32-.27 2-.27.68 0 1.36.09 2 .27 1.53-1.04 2.2-.82 2.2-.82.44 1.1.16 1.92.08 2.12.51.56.82 1.27.82 2.15 0 3.07-1.87 3.75-3.65 3.95.29.25.54.73.54 1.48 0 1.07-.01 1.93-.01 2.2 0 .21.15.46.55.38A8.013 8.013 0 0 0 16 8c0-4.42-3.58-8-8-8z"/></svg>
</a>

      </div>

    </div>

    <div class="HeaderMenu d-flex flex-justify-between flex-auto">
      <div class="d-flex">
            <div class="">
              <div class="header-search scoped-search site-scoped-search js-site-search position-relative js-jump-to"
  role="combobox"
  aria-owns="jump-to-results"
  aria-label="Search or jump to"
  aria-haspopup="listbox"
  aria-expanded="false"
>
  <div class="position-relative">
    <!-- '"` --><!-- </textarea></xmp> --></option></form><form class="js-site-search-form" data-scope-type="Repository" data-scope-id="150428711" data-scoped-search-url="/testgmex/api-doc/search" data-unscoped-search-url="/search" action="/testgmex/api-doc/search" accept-charset="UTF-8" method="get"><input name="utf8" type="hidden" value="&#x2713;" />
      <label class="form-control header-search-wrapper header-search-wrapper-jump-to position-relative d-flex flex-justify-between flex-items-center js-chromeless-input-container">
        <input type="text"
          class="form-control header-search-input jump-to-field js-jump-to-field js-site-search-focus js-site-search-field is-clearable"
          data-hotkey="s,/"
          name="q"
          value=""
          placeholder="Search or jump to…"
          data-unscoped-placeholder="Search or jump to…"
          data-scoped-placeholder="Search or jump to…"
          autocapitalize="off"
          aria-autocomplete="list"
          aria-controls="jump-to-results"
          data-jump-to-suggestions-path="/_graphql/GetSuggestedNavigationDestinations#csrf-token=HczvKHOt26rZ1sGIdnvPSGUL/Fgkbexkcr0beoNZMq8mUh2hlLCIDtrkiBrhJyAZPeynCLaRfMmot/2wFCehWQ=="
          spellcheck="false"
          autocomplete="off"
          >
          <input type="hidden" class="js-site-search-type-field" name="type" >
            <img src="https://assets-cdn.github.com/images/search-shortcut-hint.svg" alt="" class="mr-2 header-search-key-slash">

            <div class="Box position-absolute overflow-hidden d-none jump-to-suggestions js-jump-to-suggestions-container">
              <ul class="d-none js-jump-to-suggestions-template-container">
                <li class="d-flex flex-justify-start flex-items-center p-0 f5 navigation-item js-navigation-item" role="option">
                  <a tabindex="-1" class="no-underline d-flex flex-auto flex-items-center p-2 jump-to-suggestions-path js-jump-to-suggestion-path js-navigation-open" href="">
                    <div class="jump-to-octicon js-jump-to-octicon mr-2 text-center d-none">
                      <svg height="16" width="16" class="octicon octicon-repo flex-shrink-0 js-jump-to-octicon-repo d-none" title="Repository" aria-label="Repository" viewBox="0 0 12 16" version="1.1" role="img"><path fill-rule="evenodd" d="M4 9H3V8h1v1zm0-3H3v1h1V6zm0-2H3v1h1V4zm0-2H3v1h1V2zm8-1v12c0 .55-.45 1-1 1H6v2l-1.5-1.5L3 16v-2H1c-.55 0-1-.45-1-1V1c0-.55.45-1 1-1h10c.55 0 1 .45 1 1zm-1 10H1v2h2v-1h3v1h5v-2zm0-10H2v9h9V1z"/></svg>
                      <svg height="16" width="16" class="octicon octicon-project flex-shrink-0 js-jump-to-octicon-project d-none" title="Project" aria-label="Project" viewBox="0 0 15 16" version="1.1" role="img"><path fill-rule="evenodd" d="M10 12h3V2h-3v10zm-4-2h3V2H6v8zm-4 4h3V2H2v12zm-1 1h13V1H1v14zM14 0H1a1 1 0 0 0-1 1v14a1 1 0 0 0 1 1h13a1 1 0 0 0 1-1V1a1 1 0 0 0-1-1z"/></svg>
                      <svg height="16" width="16" class="octicon octicon-search flex-shrink-0 js-jump-to-octicon-search d-none" title="Search" aria-label="Search" viewBox="0 0 16 16" version="1.1" role="img"><path fill-rule="evenodd" d="M15.7 13.3l-3.81-3.83A5.93 5.93 0 0 0 13 6c0-3.31-2.69-6-6-6S1 2.69 1 6s2.69 6 6 6c1.3 0 2.48-.41 3.47-1.11l3.83 3.81c.19.2.45.3.7.3.25 0 .52-.09.7-.3a.996.996 0 0 0 0-1.41v.01zM7 10.7c-2.59 0-4.7-2.11-4.7-4.7 0-2.59 2.11-4.7 4.7-4.7 2.59 0 4.7 2.11 4.7 4.7 0 2.59-2.11 4.7-4.7 4.7z"/></svg>
                    </div>

                    <img class="avatar mr-2 flex-shrink-0 js-jump-to-suggestion-avatar d-none" alt="" aria-label="Team" src="" width="28" height="28">

                    <div class="jump-to-suggestion-name js-jump-to-suggestion-name flex-auto overflow-hidden text-left no-wrap css-truncate css-truncate-target">
                    </div>

                    <div class="border rounded-1 flex-shrink-0 bg-gray px-1 text-gray-light ml-1 f6 d-none js-jump-to-badge-search">
                      <span class="js-jump-to-badge-search-text-default d-none" aria-label="in this repository">
                        In this repository
                      </span>
                      <span class="js-jump-to-badge-search-text-global d-none" aria-label="in all of GitHub">
                        All GitHub
                      </span>
                      <span aria-hidden="true" class="d-inline-block ml-1 v-align-middle">↵</span>
                    </div>

                    <div aria-hidden="true" class="border rounded-1 flex-shrink-0 bg-gray px-1 text-gray-light ml-1 f6 d-none d-on-nav-focus js-jump-to-badge-jump">
                      Jump to
                      <span class="d-inline-block ml-1 v-align-middle">↵</span>
                    </div>
                  </a>
                </li>
              </ul>
              <ul class="d-none js-jump-to-no-results-template-container">
                <li class="d-flex flex-justify-center flex-items-center p-3 f5 d-none">
                  <span class="text-gray">No suggested jump to results</span>
                </li>
              </ul>

              <ul id="jump-to-results" role="listbox" class="js-navigation-container jump-to-suggestions-results-container js-jump-to-suggestions-results-container" >
                <li class="d-flex flex-justify-center flex-items-center p-0 f5">
                  <img src="https://assets-cdn.github.com/images/spinners/octocat-spinner-128.gif" alt="Octocat Spinner Icon" class="m-2" width="28">
                </li>
              </ul>
            </div>
      </label>
</form>  </div>
</div>

            </div>

          <ul class="d-flex pl-2 flex-items-center text-bold list-style-none" role="navigation">
            <li>
              <a class="js-selected-navigation-item HeaderNavlink px-2" data-hotkey="g p" data-ga-click="Header, click, Nav menu - item:pulls context:user" aria-label="Pull requests you created" data-selected-links="/pulls /pulls/assigned /pulls/mentioned /pulls" href="/pulls">
                Pull requests
</a>            </li>
            <li>
              <a class="js-selected-navigation-item HeaderNavlink px-2" data-hotkey="g i" data-ga-click="Header, click, Nav menu - item:issues context:user" aria-label="Issues you created" data-selected-links="/issues /issues/assigned /issues/mentioned /issues" href="/issues">
                Issues
</a>            </li>
              <li>
                <a class="js-selected-navigation-item HeaderNavlink px-2" data-ga-click="Header, click, Nav menu - item:marketplace context:user" data-octo-click="marketplace_click" data-octo-dimensions="location:nav_bar" data-selected-links=" /marketplace" href="/marketplace">
                   Marketplace
</a>              </li>
            <li>
              <a class="js-selected-navigation-item HeaderNavlink px-2" data-ga-click="Header, click, Nav menu - item:explore" data-selected-links="/explore /trending /trending/developers /integrations /integrations/feature/code /integrations/feature/collaborate /integrations/feature/ship showcases showcases_search showcases_landing /explore" href="/explore">
                Explore
</a>            </li>
          </ul>
      </div>

      <div class="d-flex">
        
<ul class="user-nav d-flex flex-items-center list-style-none" id="user-links">
  <li class="dropdown">
    <span class="d-inline-block  px-2">
      
    <a aria-label="You have no unread notifications" class="notification-indicator tooltipped tooltipped-s  js-socket-channel js-notification-indicator" data-hotkey="g n" data-ga-click="Header, go to notifications, icon:read" data-channel="notification-changed:43609835" href="/notifications">
        <span class="mail-status "></span>
        <svg class="octicon octicon-bell" viewBox="0 0 14 16" version="1.1" width="14" height="16" aria-hidden="true"><path fill-rule="evenodd" d="M13.99 11.991v1H0v-1l.73-.58c.769-.769.809-2.547 1.189-4.416.77-3.767 4.077-4.996 4.077-4.996 0-.55.45-1 .999-1 .55 0 1 .45 1 1 0 0 3.387 1.229 4.156 4.996.38 1.879.42 3.657 1.19 4.417l.659.58h-.01zM6.995 15.99c1.11 0 1.999-.89 1.999-1.999H4.996c0 1.11.89 1.999 1.999 1.999z"/></svg>
</a>
    </span>
  </li>

  <li class="dropdown">
    <details class="details-overlay details-reset d-flex px-2 flex-items-center">
      <summary class="HeaderNavlink"
         aria-label="Create new…"
         data-ga-click="Header, create new, icon:add">
        <svg class="octicon octicon-plus float-left mr-1 mt-1" viewBox="0 0 12 16" version="1.1" width="12" height="16" aria-hidden="true"><path fill-rule="evenodd" d="M12 9H7v5H5V9H0V7h5V2h2v5h5v2z"/></svg>
        <span class="dropdown-caret mt-1"></span>
      </summary>
      <details-menu class="dropdown-menu dropdown-menu-sw">
        
<a role="menuitem" class="dropdown-item" href="/new" data-ga-click="Header, create new repository">
  New repository
</a>

  <a role="menuitem" class="dropdown-item" href="/new/import" data-ga-click="Header, import a repository">
    Import repository
  </a>

<a role="menuitem" class="dropdown-item" href="https://gist.github.com/" data-ga-click="Header, create new gist">
  New gist
</a>

  <a role="menuitem" class="dropdown-item" href="/organizations/new" data-ga-click="Header, create new organization">
    New organization
  </a>


  <div class="dropdown-divider"></div>
  <div class="dropdown-header">
    <span title="testgmex/api-doc">This repository</span>
  </div>
    <a role="menuitem" class="dropdown-item" href="/testgmex/api-doc/issues/new" data-ga-click="Header, create new issue">
      New issue
    </a>


      </details-menu>
    </details>
  </li>

  <li class="dropdown">

    <details class="details-overlay details-reset d-flex pl-2 flex-items-center">
      <summary class="HeaderNavlink name mt-1"
        aria-label="View profile and more"
        data-ga-click="Header, show menu, icon:avatar">
        <img alt="@testgmex" class="avatar float-left mr-1" src="https://avatars1.githubusercontent.com/u/43609835?s=40&amp;v=4" height="20" width="20">
        <span class="dropdown-caret"></span>
      </summary>
      <details-menu class="dropdown-menu dropdown-menu-sw">
        <ul>
          <li class="header-nav-current-user css-truncate"><a role="menuitem" class="no-underline user-profile-link px-3 pt-2 pb-2 mb-n2 mt-n1 d-block" href="/testgmex" data-ga-click="Header, go to profile, text:Signed in as">Signed in as <strong class="css-truncate-target">testgmex</strong></a></li>
          <li class="dropdown-divider"></li>
          <li><a role="menuitem" class="dropdown-item" href="/testgmex" data-ga-click="Header, go to profile, text:your profile">Your profile</a></li>
          <li><a role="menuitem" class="dropdown-item" href="/testgmex?tab=repositories" data-ga-click="Header, go to repositories, text:your repositories">Your repositories</a></li>


          <li><a role="menuitem" class="dropdown-item" href="/testgmex?tab=stars" data-ga-click="Header, go to starred repos, text:your stars">Your stars</a></li>
            <li><a role="menuitem" class="dropdown-item" href="https://gist.github.com/" data-ga-click="Header, your gists, text:your gists">Your gists</a></li>
          <li class="dropdown-divider"></li>
          <li><a role="menuitem" class="dropdown-item" href="https://help.github.com" data-ga-click="Header, go to help, text:help">Help</a></li>
          <li><a role="menuitem" class="dropdown-item" href="/settings/profile" data-ga-click="Header, go to settings, icon:settings">Settings</a></li>
          <li>
            <!-- '"` --><!-- </textarea></xmp> --></option></form><form class="logout-form" action="/logout" accept-charset="UTF-8" method="post"><input name="utf8" type="hidden" value="&#x2713;" /><input type="hidden" name="authenticity_token" value="q02SqQFrHEhHJdH8B1IsjbKkxnHOIDJvTpTZWn78zgxNC+AUG4rCS24zs1F55Osu214fsUaIzmUkgFVX5tS6lg==" />
              <button type="submit" class="dropdown-item dropdown-signout" data-ga-click="Header, sign out, icon:logout" role="menuitem">
                Sign out
              </button>
</form>          </li>
        </ul>
      </details-menu>
    </details>
  </li>
</ul>



        <!-- '"` --><!-- </textarea></xmp> --></option></form><form class="sr-only right-0" action="/logout" accept-charset="UTF-8" method="post"><input name="utf8" type="hidden" value="&#x2713;" /><input type="hidden" name="authenticity_token" value="my0ptk0gTSoE1yP9CzImugZJUHeDLSkIofAWPgafPLF9a1sLV8GTKS3BQVB1hOEZb7OJtwuF1QLL5JoznrdIKw==" />
          <button type="submit" class="dropdown-item dropdown-signout" data-ga-click="Header, sign out, icon:logout">
            Sign out
          </button>
</form>      </div>
    </div>
  </div>
</header>

      

  </div>

  <div id="start-of-content" class="show-on-focus"></div>

    <div id="js-flash-container">


</div>



  <div role="main" class="application-main ">
        <div itemscope itemtype="http://schema.org/SoftwareSourceCode" class="">
    <div id="js-repo-pjax-container" data-pjax-container >
      








  <div class="pagehead repohead instapaper_ignore readability-menu experiment-repo-nav  ">
    <div class="repohead-details-container clearfix container">

      <ul class="pagehead-actions">
  <li>
        <!-- '"` --><!-- </textarea></xmp> --></option></form><form data-remote="true" class="js-social-form js-social-container" action="/notifications/subscribe" accept-charset="UTF-8" method="post"><input name="utf8" type="hidden" value="&#x2713;" /><input type="hidden" name="authenticity_token" value="fr+28SzHu5XHIaiNBb1RosU8uuMsPSSspqpmHE/W1qjKo2v8IFB3N+QRKPWM9ZSZ5VYKe4ad3/Z113I9GBpZ9w==" />      <input type="hidden" name="repository_id" id="repository_id" value="150428711" class="form-control" />

      <details class="details-reset details-overlay select-menu float-left">
        <summary class="btn btn-sm btn-with-count select-menu-button" data-ga-click="Repository, click Watch settings, action:blob#show">
          <span data-menu-button>
              <svg class="octicon octicon-eye v-align-text-bottom" viewBox="0 0 16 16" version="1.1" width="16" height="16" aria-hidden="true"><path fill-rule="evenodd" d="M8.06 2C3 2 0 8 0 8s3 6 8.06 6C13 14 16 8 16 8s-3-6-7.94-6zM8 12c-2.2 0-4-1.78-4-4 0-2.2 1.8-4 4-4 2.22 0 4 1.8 4 4 0 2.22-1.78 4-4 4zm2-4c0 1.11-.89 2-2 2-1.11 0-2-.89-2-2 0-1.11.89-2 2-2 1.11 0 2 .89 2 2z"/></svg>
              Watch
          </span>
        </summary>
        <details-menu class="select-menu-modal position-absolute mt-5" style="z-index: 99;">
          <div class="select-menu-header">
            <span class="select-menu-title">Notifications</span>
          </div>
          <div class="select-menu-list">
            <button type="submit" name="do" value="included" class="select-menu-item width-full" aria-checked="true" role="menuitemradio">
              <svg class="octicon octicon-check select-menu-item-icon" viewBox="0 0 12 16" version="1.1" width="12" height="16" aria-hidden="true"><path fill-rule="evenodd" d="M12 5l-8 8-4-4 1.5-1.5L4 10l6.5-6.5L12 5z"/></svg>
              <div class="select-menu-item-text">
                <span class="select-menu-item-heading">Not watching</span>
                <span class="description">Be notified when participating or @mentioned.</span>
                <span class="hidden-select-button-text" data-menu-button-contents>
                  <svg class="octicon octicon-eye v-align-text-bottom" viewBox="0 0 16 16" version="1.1" width="16" height="16" aria-hidden="true"><path fill-rule="evenodd" d="M8.06 2C3 2 0 8 0 8s3 6 8.06 6C13 14 16 8 16 8s-3-6-7.94-6zM8 12c-2.2 0-4-1.78-4-4 0-2.2 1.8-4 4-4 2.22 0 4 1.8 4 4 0 2.22-1.78 4-4 4zm2-4c0 1.11-.89 2-2 2-1.11 0-2-.89-2-2 0-1.11.89-2 2-2 1.11 0 2 .89 2 2z"/></svg>
                  Watch
                </span>
              </div>
            </button>

            <button type="submit" name="do" value="subscribed" class="select-menu-item width-full" aria-checked="false" role="menuitemradio">
              <svg class="octicon octicon-check select-menu-item-icon" viewBox="0 0 12 16" version="1.1" width="12" height="16" aria-hidden="true"><path fill-rule="evenodd" d="M12 5l-8 8-4-4 1.5-1.5L4 10l6.5-6.5L12 5z"/></svg>
              <div class="select-menu-item-text">
                <span class="select-menu-item-heading">Watching</span>
                <span class="description">Be notified of all conversations.</span>
                <span class="hidden-select-button-text" data-menu-button-contents>
                  <svg class="octicon octicon-eye v-align-text-bottom" viewBox="0 0 16 16" version="1.1" width="16" height="16" aria-hidden="true"><path fill-rule="evenodd" d="M8.06 2C3 2 0 8 0 8s3 6 8.06 6C13 14 16 8 16 8s-3-6-7.94-6zM8 12c-2.2 0-4-1.78-4-4 0-2.2 1.8-4 4-4 2.22 0 4 1.8 4 4 0 2.22-1.78 4-4 4zm2-4c0 1.11-.89 2-2 2-1.11 0-2-.89-2-2 0-1.11.89-2 2-2 1.11 0 2 .89 2 2z"/></svg>
                  Unwatch
                </span>
              </div>
            </button>

            <button type="submit" name="do" value="ignore" class="select-menu-item width-full" aria-checked="false" role="menuitemradio">
              <svg class="octicon octicon-check select-menu-item-icon" viewBox="0 0 12 16" version="1.1" width="12" height="16" aria-hidden="true"><path fill-rule="evenodd" d="M12 5l-8 8-4-4 1.5-1.5L4 10l6.5-6.5L12 5z"/></svg>
              <div class="select-menu-item-text">
                <span class="select-menu-item-heading">Ignoring</span>
                <span class="description">Never be notified.</span>
                <span class="hidden-select-button-text" data-menu-button-contents>
                  <svg class="octicon octicon-mute v-align-text-bottom" viewBox="0 0 16 16" version="1.1" width="16" height="16" aria-hidden="true"><path fill-rule="evenodd" d="M8 2.81v10.38c0 .67-.81 1-1.28.53L3 10H1c-.55 0-1-.45-1-1V7c0-.55.45-1 1-1h2l3.72-3.72C7.19 1.81 8 2.14 8 2.81zm7.53 3.22l-1.06-1.06-1.97 1.97-1.97-1.97-1.06 1.06L11.44 8 9.47 9.97l1.06 1.06 1.97-1.97 1.97 1.97 1.06-1.06L13.56 8l1.97-1.97z"/></svg>
                  Stop ignoring
                </span>
              </div>
            </button>
          </div>
        </details-menu>
      </details>
      <a class="social-count js-social-count"
        href="/testgmex/api-doc/watchers"
        aria-label="0 users are watching this repository">
        0
      </a>
</form>
  </li>

  <li>
    
  <div class="js-toggler-container js-social-container starring-container ">
    <!-- '"` --><!-- </textarea></xmp> --></option></form><form class="starred js-social-form" action="/testgmex/api-doc/unstar" accept-charset="UTF-8" method="post"><input name="utf8" type="hidden" value="&#x2713;" /><input type="hidden" name="authenticity_token" value="08du5ayTBRv5Qw4p7qL/v5o7pbhTglVoRVqCdfPKVTTkwF/jeSB09hDK8+Jf9ZL0Vr+nIIEZ8SPDDp2ovnS+dg==" />
      <input type="hidden" name="context" value="repository"></input>
      <button
        type="submit"
        class="btn btn-sm btn-with-count js-toggler-target"
        aria-label="Unstar this repository" title="Unstar testgmex/api-doc"
        data-ga-click="Repository, click unstar button, action:blob#show; text:Unstar">
        <svg class="octicon octicon-star v-align-text-bottom" viewBox="0 0 14 16" version="1.1" width="14" height="16" aria-hidden="true"><path fill-rule="evenodd" d="M14 6l-4.9-.64L7 1 4.9 5.36 0 6l3.6 3.26L2.67 14 7 11.67 11.33 14l-.93-4.74L14 6z"/></svg>
        Unstar
      </button>
        <a class="social-count js-social-count" href="/testgmex/api-doc/stargazers"
           aria-label="0 users starred this repository">
          0
        </a>
</form>
    <!-- '"` --><!-- </textarea></xmp> --></option></form><form class="unstarred js-social-form" action="/testgmex/api-doc/star" accept-charset="UTF-8" method="post"><input name="utf8" type="hidden" value="&#x2713;" /><input type="hidden" name="authenticity_token" value="B3zGG+DvkvMuqMw+SIoH6GNKu1K8e49G4Dz6nBbtbofF+sUlAydUXye9X+IyYAtAVNUFB3I2X3l2AQJ9Fcpleg==" />
      <input type="hidden" name="context" value="repository"></input>
      <button
        type="submit"
        class="btn btn-sm btn-with-count js-toggler-target"
        aria-label="Star this repository" title="Star testgmex/api-doc"
        data-ga-click="Repository, click star button, action:blob#show; text:Star">
        <svg class="octicon octicon-star v-align-text-bottom" viewBox="0 0 14 16" version="1.1" width="14" height="16" aria-hidden="true"><path fill-rule="evenodd" d="M14 6l-4.9-.64L7 1 4.9 5.36 0 6l3.6 3.26L2.67 14 7 11.67 11.33 14l-.93-4.74L14 6z"/></svg>
        Star
      </button>
        <a class="social-count js-social-count" href="/testgmex/api-doc/stargazers"
           aria-label="0 users starred this repository">
          0
        </a>
</form>  </div>

  </li>

  <li>
        <span class="btn btn-sm btn-with-count disabled tooltipped tooltipped-sw" aria-label="Cannot fork because you own this repository and are not a member of any organizations.">
          <svg class="octicon octicon-repo-forked v-align-text-bottom" viewBox="0 0 10 16" version="1.1" width="10" height="16" aria-hidden="true"><path fill-rule="evenodd" d="M8 1a1.993 1.993 0 0 0-1 3.72V6L5 8 3 6V4.72A1.993 1.993 0 0 0 2 1a1.993 1.993 0 0 0-1 3.72V6.5l3 3v1.78A1.993 1.993 0 0 0 5 15a1.993 1.993 0 0 0 1-3.72V9.5l3-3V4.72A1.993 1.993 0 0 0 8 1zM2 4.2C1.34 4.2.8 3.65.8 3c0-.65.55-1.2 1.2-1.2.65 0 1.2.55 1.2 1.2 0 .65-.55 1.2-1.2 1.2zm3 10c-.66 0-1.2-.55-1.2-1.2 0-.65.55-1.2 1.2-1.2.65 0 1.2.55 1.2 1.2 0 .65-.55 1.2-1.2 1.2zm3-10c-.66 0-1.2-.55-1.2-1.2 0-.65.55-1.2 1.2-1.2.65 0 1.2.55 1.2 1.2 0 .65-.55 1.2-1.2 1.2z"/></svg>
          Fork
</span>
    <a href="/testgmex/api-doc/network/members" class="social-count"
       aria-label="0 users forked this repository">
      0
    </a>
  </li>
</ul>

      <h1 class="public ">
  <svg class="octicon octicon-repo" viewBox="0 0 12 16" version="1.1" width="12" height="16" aria-hidden="true"><path fill-rule="evenodd" d="M4 9H3V8h1v1zm0-3H3v1h1V6zm0-2H3v1h1V4zm0-2H3v1h1V2zm8-1v12c0 .55-.45 1-1 1H6v2l-1.5-1.5L3 16v-2H1c-.55 0-1-.45-1-1V1c0-.55.45-1 1-1h10c.55 0 1 .45 1 1zm-1 10H1v2h2v-1h3v1h5v-2zm0-10H2v9h9V1z"/></svg>
  <span class="author" itemprop="author"><a class="url fn" rel="author" href="/testgmex">testgmex</a></span><!--
--><span class="path-divider">/</span><!--
--><strong itemprop="name"><a data-pjax="#js-repo-pjax-container" href="/testgmex/api-doc">api-doc</a></strong>

</h1>

    </div>
    
<nav class="reponav js-repo-nav js-sidenav-container-pjax container"
     itemscope
     itemtype="http://schema.org/BreadcrumbList"
     role="navigation"
     data-pjax="#js-repo-pjax-container">

  <span itemscope itemtype="http://schema.org/ListItem" itemprop="itemListElement">
    <a class="js-selected-navigation-item selected reponav-item" itemprop="url" data-hotkey="g c" data-selected-links="repo_source repo_downloads repo_commits repo_releases repo_tags repo_branches repo_packages /testgmex/api-doc" href="/testgmex/api-doc">
      <svg class="octicon octicon-code" viewBox="0 0 14 16" version="1.1" width="14" height="16" aria-hidden="true"><path fill-rule="evenodd" d="M9.5 3L8 4.5 11.5 8 8 11.5 9.5 13 14 8 9.5 3zm-5 0L0 8l4.5 5L6 11.5 2.5 8 6 4.5 4.5 3z"/></svg>
      <span itemprop="name">Code</span>
      <meta itemprop="position" content="1">
</a>  </span>

    <span itemscope itemtype="http://schema.org/ListItem" itemprop="itemListElement">
      <a itemprop="url" data-hotkey="g i" class="js-selected-navigation-item reponav-item" data-selected-links="repo_issues repo_labels repo_milestones /testgmex/api-doc/issues" href="/testgmex/api-doc/issues">
        <svg class="octicon octicon-issue-opened" viewBox="0 0 14 16" version="1.1" width="14" height="16" aria-hidden="true"><path fill-rule="evenodd" d="M7 2.3c3.14 0 5.7 2.56 5.7 5.7s-2.56 5.7-5.7 5.7A5.71 5.71 0 0 1 1.3 8c0-3.14 2.56-5.7 5.7-5.7zM7 1C3.14 1 0 4.14 0 8s3.14 7 7 7 7-3.14 7-7-3.14-7-7-7zm1 3H6v5h2V4zm0 6H6v2h2v-2z"/></svg>
        <span itemprop="name">Issues</span>
        <span class="Counter">0</span>
        <meta itemprop="position" content="2">
</a>    </span>

  <span itemscope itemtype="http://schema.org/ListItem" itemprop="itemListElement">
    <a data-hotkey="g p" itemprop="url" class="js-selected-navigation-item reponav-item" data-selected-links="repo_pulls checks /testgmex/api-doc/pulls" href="/testgmex/api-doc/pulls">
      <svg class="octicon octicon-git-pull-request" viewBox="0 0 12 16" version="1.1" width="12" height="16" aria-hidden="true"><path fill-rule="evenodd" d="M11 11.28V5c-.03-.78-.34-1.47-.94-2.06C9.46 2.35 8.78 2.03 8 2H7V0L4 3l3 3V4h1c.27.02.48.11.69.31.21.2.3.42.31.69v6.28A1.993 1.993 0 0 0 10 15a1.993 1.993 0 0 0 1-3.72zm-1 2.92c-.66 0-1.2-.55-1.2-1.2 0-.65.55-1.2 1.2-1.2.65 0 1.2.55 1.2 1.2 0 .65-.55 1.2-1.2 1.2zM4 3c0-1.11-.89-2-2-2a1.993 1.993 0 0 0-1 3.72v6.56A1.993 1.993 0 0 0 2 15a1.993 1.993 0 0 0 1-3.72V4.72c.59-.34 1-.98 1-1.72zm-.8 10c0 .66-.55 1.2-1.2 1.2-.65 0-1.2-.55-1.2-1.2 0-.65.55-1.2 1.2-1.2.65 0 1.2.55 1.2 1.2zM2 4.2C1.34 4.2.8 3.65.8 3c0-.65.55-1.2 1.2-1.2.65 0 1.2.55 1.2 1.2 0 .65-.55 1.2-1.2 1.2z"/></svg>
      <span itemprop="name">Pull requests</span>
      <span class="Counter">0</span>
      <meta itemprop="position" content="3">
</a>  </span>


    <a data-hotkey="g b" class="js-selected-navigation-item reponav-item" data-selected-links="repo_projects new_repo_project repo_project /testgmex/api-doc/projects" href="/testgmex/api-doc/projects">
      <svg class="octicon octicon-project" viewBox="0 0 15 16" version="1.1" width="15" height="16" aria-hidden="true"><path fill-rule="evenodd" d="M10 12h3V2h-3v10zm-4-2h3V2H6v8zm-4 4h3V2H2v12zm-1 1h13V1H1v14zM14 0H1a1 1 0 0 0-1 1v14a1 1 0 0 0 1 1h13a1 1 0 0 0 1-1V1a1 1 0 0 0-1-1z"/></svg>
      Projects
      <span class="Counter" >0</span>
</a>

    <a class="js-selected-navigation-item reponav-item" data-hotkey="g w" data-selected-links="repo_wiki /testgmex/api-doc/wiki" href="/testgmex/api-doc/wiki">
      <svg class="octicon octicon-book" viewBox="0 0 16 16" version="1.1" width="16" height="16" aria-hidden="true"><path fill-rule="evenodd" d="M3 5h4v1H3V5zm0 3h4V7H3v1zm0 2h4V9H3v1zm11-5h-4v1h4V5zm0 2h-4v1h4V7zm0 2h-4v1h4V9zm2-6v9c0 .55-.45 1-1 1H9.5l-1 1-1-1H2c-.55 0-1-.45-1-1V3c0-.55.45-1 1-1h5.5l1 1 1-1H15c.55 0 1 .45 1 1zm-8 .5L7.5 3H2v9h6V3.5zm7-.5H9.5l-.5.5V12h6V3z"/></svg>
      Wiki
</a>
  <a class="js-selected-navigation-item reponav-item" data-selected-links="repo_graphs repo_contributors dependency_graph pulse alerts /testgmex/api-doc/pulse" href="/testgmex/api-doc/pulse">
    <svg class="octicon octicon-graph" viewBox="0 0 16 16" version="1.1" width="16" height="16" aria-hidden="true"><path fill-rule="evenodd" d="M16 14v1H0V0h1v14h15zM5 13H3V8h2v5zm4 0H7V3h2v10zm4 0h-2V6h2v7z"/></svg>
    Insights
</a>
    <a class="js-selected-navigation-item reponav-item" data-selected-links="repo_settings repo_branch_settings hooks integration_installations repo_keys_settings issue_template_editor /testgmex/api-doc/settings" href="/testgmex/api-doc/settings">
      <svg class="octicon octicon-gear" viewBox="0 0 14 16" version="1.1" width="14" height="16" aria-hidden="true"><path fill-rule="evenodd" d="M14 8.77v-1.6l-1.94-.64-.45-1.09.88-1.84-1.13-1.13-1.81.91-1.09-.45-.69-1.92h-1.6l-.63 1.94-1.11.45-1.84-.88-1.13 1.13.91 1.81-.45 1.09L0 7.23v1.59l1.94.64.45 1.09-.88 1.84 1.13 1.13 1.81-.91 1.09.45.69 1.92h1.59l.63-1.94 1.11-.45 1.84.88 1.13-1.13-.92-1.81.47-1.09L14 8.75v.02zM7 11c-1.66 0-3-1.34-3-3s1.34-3 3-3 3 1.34 3 3-1.34 3-3 3z"/></svg>
      Settings
</a>
</nav>


  </div>

<div class="container new-discussion-timeline experiment-repo-nav  ">
  <div class="repository-content ">

    
  <a class="d-none js-permalink-shortcut" data-hotkey="y" href="/testgmex/api-doc/blob/8547f090917e3df30d46462a417a8b7c004c31dc/WebSocket_API_for_GMEX_v1.md">Permalink</a>

  <!-- blob contrib key: blob_contributors:v21:2a2fdaf0712ddd3812356b7073a0848a -->

  

  <div class="file-navigation">
    
<div class="select-menu branch-select-menu js-menu-container js-select-menu float-left">
  <button class=" btn btn-sm select-menu-button js-menu-target css-truncate" data-hotkey="w"
    
    type="button" aria-label="Switch branches or tags" aria-expanded="false" aria-haspopup="true">
      <i>Branch:</i>
      <span class="js-select-button css-truncate-target">master</span>
  </button>

  <div class="select-menu-modal-holder js-menu-content js-navigation-container" data-pjax>

    <div class="select-menu-modal">
      <div class="select-menu-header">
        <svg class="octicon octicon-x js-menu-close" role="img" aria-label="Close" viewBox="0 0 12 16" version="1.1" width="12" height="16"><path fill-rule="evenodd" d="M7.48 8l3.75 3.75-1.48 1.48L6 9.48l-3.75 3.75-1.48-1.48L4.52 8 .77 4.25l1.48-1.48L6 6.52l3.75-3.75 1.48 1.48L7.48 8z"/></svg>
        <span class="select-menu-title">Switch branches/tags</span>
      </div>

      <div class="select-menu-filters">
        <div class="select-menu-text-filter">
          <input type="text" aria-label="Find or create a branch…" id="context-commitish-filter-field" class="form-control js-filterable-field js-navigation-enable" placeholder="Find or create a branch…">
        </div>
        <div class="select-menu-tabs">
          <ul>
            <li class="select-menu-tab">
              <a href="#" data-tab-filter="branches" data-filter-placeholder="Find or create a branch…" class="js-select-menu-tab" role="tab">Branches</a>
            </li>
            <li class="select-menu-tab">
              <a href="#" data-tab-filter="tags" data-filter-placeholder="Find a tag…" class="js-select-menu-tab" role="tab">Tags</a>
            </li>
          </ul>
        </div>
      </div>

      <div class="select-menu-list select-menu-tab-bucket js-select-menu-tab-bucket" data-tab-filter="branches" role="menu">

        <div data-filterable-for="context-commitish-filter-field" data-filterable-type="substring">


            <a class="select-menu-item js-navigation-item js-navigation-open selected"
               href="/testgmex/api-doc/blob/master/WebSocket_API_for_GMEX_v1.md"
               data-name="master"
               data-skip-pjax="true"
               rel="nofollow">
              <svg class="octicon octicon-check select-menu-item-icon" viewBox="0 0 12 16" version="1.1" width="12" height="16" aria-hidden="true"><path fill-rule="evenodd" d="M12 5l-8 8-4-4 1.5-1.5L4 10l6.5-6.5L12 5z"/></svg>
              <span class="select-menu-item-text css-truncate-target js-select-menu-filter-text">
                master
              </span>
            </a>
        </div>

          <!-- '"` --><!-- </textarea></xmp> --></option></form><form class="select-menu-new-item-form js-new-item-form" action="/testgmex/api-doc/branches" accept-charset="UTF-8" method="post"><input name="utf8" type="hidden" value="&#x2713;" /><input type="hidden" name="authenticity_token" value="kzVkCqlCTrSwsEmOrBJJkWvP4VTr83n0S/0Ejnrf8Pj3xug1vtpKA+7N8O7nKrDkZu2hJkr4pw2SNoa1vYzneQ==" />
            <input type="hidden" name="name" id="name" class="js-new-item-value">
            <input type="hidden" name="branch" id="branch" value="master">
            <input type="hidden" name="path" id="path" value="WebSocket_API_for_GMEX_v1.md">

            <button type="submit" class="width-full select-menu-item js-navigation-open js-navigation-item">
              <svg class="octicon octicon-git-branch select-menu-item-icon" viewBox="0 0 10 16" version="1.1" width="10" height="16" aria-hidden="true"><path fill-rule="evenodd" d="M10 5c0-1.11-.89-2-2-2a1.993 1.993 0 0 0-1 3.72v.3c-.02.52-.23.98-.63 1.38-.4.4-.86.61-1.38.63-.83.02-1.48.16-2 .45V4.72a1.993 1.993 0 0 0-1-3.72C.88 1 0 1.89 0 3a2 2 0 0 0 1 1.72v6.56c-.59.35-1 .99-1 1.72 0 1.11.89 2 2 2 1.11 0 2-.89 2-2 0-.53-.2-1-.53-1.36.09-.06.48-.41.59-.47.25-.11.56-.17.94-.17 1.05-.05 1.95-.45 2.75-1.25S8.95 7.77 9 6.73h-.02C9.59 6.37 10 5.73 10 5zM2 1.8c.66 0 1.2.55 1.2 1.2 0 .65-.55 1.2-1.2 1.2C1.35 4.2.8 3.65.8 3c0-.65.55-1.2 1.2-1.2zm0 12.41c-.66 0-1.2-.55-1.2-1.2 0-.65.55-1.2 1.2-1.2.65 0 1.2.55 1.2 1.2 0 .65-.55 1.2-1.2 1.2zm6-8c-.66 0-1.2-.55-1.2-1.2 0-.65.55-1.2 1.2-1.2.65 0 1.2.55 1.2 1.2 0 .65-.55 1.2-1.2 1.2z"/></svg>
              <div class="select-menu-item-text">
                <span class="select-menu-item-heading">Create branch: <span class="js-new-item-name"></span></span>
                <span class="description">from ‘master’</span>
              </div>
            </button>
</form>
      </div>

      <div class="select-menu-list select-menu-tab-bucket js-select-menu-tab-bucket" data-tab-filter="tags">
        <div data-filterable-for="context-commitish-filter-field" data-filterable-type="substring">


        </div>

        <div class="select-menu-no-results">Nothing to show</div>
      </div>

    </div>
  </div>
</div>

    <div class="BtnGroup float-right">
      <a href="/testgmex/api-doc/find/master"
            class="js-pjax-capture-input btn btn-sm BtnGroup-item"
            data-pjax
            data-hotkey="t">
        Find file
      </a>
      <clipboard-copy for="blob-path" class="btn btn-sm BtnGroup-item">
        Copy path
      </clipboard-copy>
    </div>
    <div id="blob-path" class="breadcrumb">
      <span class="repo-root js-repo-root"><span class="js-path-segment"><a data-pjax="true" href="/testgmex/api-doc"><span>api-doc</span></a></span></span><span class="separator">/</span><strong class="final-path">WebSocket_API_for_GMEX_v1.md</strong>
    </div>
  </div>


  
  <div class="commit-tease">
      <span class="float-right">
        <a class="commit-tease-sha" href="/testgmex/api-doc/commit/8547f090917e3df30d46462a417a8b7c004c31dc" data-pjax>
          8547f09
        </a>
        <relative-time datetime="2018-09-30T07:32:01Z">Sep 30, 2018</relative-time>
      </span>
      <div>
        <a rel="author" data-skip-pjax="true" data-hovercard-type="user" data-hovercard-url="/hovercards?user_id=43609835" data-octo-click="hovercard-link-click" data-octo-dimensions="link_type:self" href="/testgmex"><img class="avatar" src="https://avatars1.githubusercontent.com/u/43609835?s=40&amp;v=4" width="20" height="20" alt="@testgmex" /></a>
        <a class="user-mention" rel="author" data-hovercard-type="user" data-hovercard-url="/hovercards?user_id=43609835" data-octo-click="hovercard-link-click" data-octo-dimensions="link_type:self" href="/testgmex">testgmex</a>
          <a data-pjax="true" title="Update WebSocket_API_for_GMEX_v1.md" class="message" href="/testgmex/api-doc/commit/8547f090917e3df30d46462a417a8b7c004c31dc">Update WebSocket_API_for_GMEX_v1.md</a>
      </div>

    <div class="commit-tease-contributors">
      
<details class="details-reset details-overlay details-overlay-dark lh-default text-gray-dark float-left mr-2" id="blob_contributors_box">
  <summary class="btn-link" aria-haspopup="dialog"  >
    
    <span><strong>1</strong> contributor</span>
  </summary>
  <details-dialog class="Box Box--overlay d-flex flex-column anim-fade-in fast " aria-label="Users who have contributed to this file">
    <div class="Box-header">
      <button class="Box-btn-octicon btn-octicon float-right" type="button" aria-label="Close dialog" data-close-dialog>
        <svg class="octicon octicon-x" viewBox="0 0 12 16" version="1.1" width="12" height="16" aria-hidden="true"><path fill-rule="evenodd" d="M7.48 8l3.75 3.75-1.48 1.48L6 9.48l-3.75 3.75-1.48-1.48L4.52 8 .77 4.25l1.48-1.48L6 6.52l3.75-3.75 1.48 1.48L7.48 8z"/></svg>
      </button>
      <h3 class="Box-title">Users who have contributed to this file</h3>
    </div>
    
        <ul class="list-style-none overflow-auto">
            <li class="Box-row">
              <a class="link-gray-dark no-underline" href="/testgmex">
                <img class="avatar mr-2" alt="" src="https://avatars1.githubusercontent.com/u/43609835?s=40&amp;v=4" width="20" height="20" />
                testgmex
</a>            </li>
        </ul>

  </details-dialog>
</details>
      
    </div>
  </div>



  <div class="file">
    <div class="file-header">
  <div class="file-actions">

    <div class="BtnGroup">
      <a id="raw-url" class="btn btn-sm BtnGroup-item" href="/testgmex/api-doc/raw/master/WebSocket_API_for_GMEX_v1.md">Raw</a>
        <a class="btn btn-sm js-update-url-with-hash BtnGroup-item" data-hotkey="b" href="/testgmex/api-doc/blame/master/WebSocket_API_for_GMEX_v1.md">Blame</a>
      <a rel="nofollow" class="btn btn-sm BtnGroup-item" href="/testgmex/api-doc/commits/master/WebSocket_API_for_GMEX_v1.md">History</a>
    </div>

        <a class="btn-octicon tooltipped tooltipped-nw"
           href="https://desktop.github.com"
           aria-label="Open this file in GitHub Desktop"
           data-ga-click="Repository, open with desktop, type:windows">
            <svg class="octicon octicon-device-desktop" viewBox="0 0 16 16" version="1.1" width="16" height="16" aria-hidden="true"><path fill-rule="evenodd" d="M15 2H1c-.55 0-1 .45-1 1v9c0 .55.45 1 1 1h5.34c-.25.61-.86 1.39-2.34 2h8c-1.48-.61-2.09-1.39-2.34-2H15c.55 0 1-.45 1-1V3c0-.55-.45-1-1-1zm0 9H1V3h14v8z"/></svg>
        </a>

          <!-- '"` --><!-- </textarea></xmp> --></option></form><form class="inline-form js-update-url-with-hash" action="/testgmex/api-doc/edit/master/WebSocket_API_for_GMEX_v1.md" accept-charset="UTF-8" method="post"><input name="utf8" type="hidden" value="&#x2713;" /><input type="hidden" name="authenticity_token" value="ye5eChjiHMK8B7HzMkIgTIjBDOFG5CuJ3IfLETBx27KZPi6LZ1rx+M/9tlZn6n9GTkX24oVNab/5U64CrjQ89A==" />
            <button class="btn-octicon tooltipped tooltipped-nw" type="submit"
              aria-label="Edit this file" data-hotkey="e" data-disable-with>
              <svg class="octicon octicon-pencil" viewBox="0 0 14 16" version="1.1" width="14" height="16" aria-hidden="true"><path fill-rule="evenodd" d="M0 12v3h3l8-8-3-3-8 8zm3 2H1v-2h1v1h1v1zm10.3-9.3L12 6 9 3l1.3-1.3a.996.996 0 0 1 1.41 0l1.59 1.59c.39.39.39 1.02 0 1.41z"/></svg>
            </button>
</form>
        <!-- '"` --><!-- </textarea></xmp> --></option></form><form class="inline-form" action="/testgmex/api-doc/delete/master/WebSocket_API_for_GMEX_v1.md" accept-charset="UTF-8" method="post"><input name="utf8" type="hidden" value="&#x2713;" /><input type="hidden" name="authenticity_token" value="E6RjclL2CW3B4ZR5XqVy+TMMdHIAJ/Ng9n9Zt+rKEppi6n8sVV8u24se3+mzEvVE7KHTWqVpDjDad+BNyraldg==" />
          <button class="btn-octicon btn-octicon-danger tooltipped tooltipped-nw" type="submit"
            aria-label="Delete this file" data-disable-with>
            <svg class="octicon octicon-trashcan" viewBox="0 0 12 16" version="1.1" width="12" height="16" aria-hidden="true"><path fill-rule="evenodd" d="M11 2H9c0-.55-.45-1-1-1H5c-.55 0-1 .45-1 1H2c-.55 0-1 .45-1 1v1c0 .55.45 1 1 1v9c0 .55.45 1 1 1h7c.55 0 1-.45 1-1V5c.55 0 1-.45 1-1V3c0-.55-.45-1-1-1zm-1 12H3V5h1v8h1V5h1v8h1V5h1v8h1V5h1v9zm1-10H2V3h9v1z"/></svg>
          </button>
</form>  </div>

  <div class="file-info">
      546 lines (463 sloc)
      <span class="file-info-divider"></span>
    31.8 KB
  </div>
</div>

    
  <div id="readme" class="readme blob instapaper_body">
    <article class="markdown-body entry-content" itemprop="text"><h1><a id="user-content-gmex-websocket-api-v1-beta" class="anchor" aria-hidden="true" href="#gmex-websocket-api-v1-beta"><svg class="octicon octicon-link" viewBox="0 0 16 16" version="1.1" width="16" height="16" aria-hidden="true"><path fill-rule="evenodd" d="M4 9h1v1H4c-1.5 0-3-1.69-3-3.5S2.55 3 4 3h4c1.45 0 3 1.69 3 3.5 0 1.41-.91 2.72-2 3.25V8.59c.58-.45 1-1.27 1-2.09C10 5.22 8.98 4 8 4H4c-.98 0-2 1.22-2 2.5S3 9 4 9zm9-3h-1v1h1c1 0 2 1.22 2 2.5S13.98 12 13 12H9c-.98 0-2-1.22-2-2.5 0-.83.42-1.64 1-2.09V6.25c-1.09.53-2 1.84-2 3.25C6 11.31 7.55 13 9 13h4c1.45 0 3-1.69 3-3.5S14.5 6 13 6z"></path></svg></a>GMEX WebSocket API (v1) beta</h1>
<h2><a id="user-content-说明" class="anchor" aria-hidden="true" href="#说明"><svg class="octicon octicon-link" viewBox="0 0 16 16" version="1.1" width="16" height="16" aria-hidden="true"><path fill-rule="evenodd" d="M4 9h1v1H4c-1.5 0-3-1.69-3-3.5S2.55 3 4 3h4c1.45 0 3 1.69 3 3.5 0 1.41-.91 2.72-2 3.25V8.59c.58-.45 1-1.27 1-2.09C10 5.22 8.98 4 8 4H4c-.98 0-2 1.22-2 2.5S3 9 4 9zm9-3h-1v1h1c1 0 2 1.22 2 2.5S13.98 12 13 12H9c-.98 0-2-1.22-2-2.5 0-.83.42-1.64 1-2.09V6.25c-1.09.53-2 1.84-2 3.25C6 11.31 7.55 13 9 13h4c1.45 0 3-1.69 3-3.5S14.5 6 13 6z"></path></svg></a>说明</h2>
<p>目前 GMEX (<a href="https://www.gmex.io" rel="nofollow">https://www.gmex.io</a>) 对于外提供 WebSocket API 开发接口， 供开发者获取行情数据和进行交易操作。
请注意，行情和交易两个服务是分开的，行情接口无需认证可以自由访问，交易部分则需要用户开通API-KEY后通过自己的KEY认证授权后方可使用。</p>
<p>GMEX官方的生产环境(暂未开放)：</p>
<pre><code>官方网址： https://www.gmex.io
行情服务： wss://api-market.gmex.io/v1/market
交易服务： wss://api-trade.gmex.io/v1/trade
</code></pre>
<p>为方便大家测试，官方提供模拟环境:</p>
<pre><code>模拟网址： https://simgo.gmex.io
模拟行情： wss://market01.gmex.io/v1/market
模拟交易： wss://trade01.gmex.io/v1/trade
</code></pre>
<h2><a id="user-content-行情api" class="anchor" aria-hidden="true" href="#行情api"><svg class="octicon octicon-link" viewBox="0 0 16 16" version="1.1" width="16" height="16" aria-hidden="true"><path fill-rule="evenodd" d="M4 9h1v1H4c-1.5 0-3-1.69-3-3.5S2.55 3 4 3h4c1.45 0 3 1.69 3 3.5 0 1.41-.91 2.72-2 3.25V8.59c.58-.45 1-1.27 1-2.09C10 5.22 8.98 4 8 4H4c-.98 0-2 1.22-2 2.5S3 9 4 9zm9-3h-1v1h1c1 0 2 1.22 2 2.5S13.98 12 13 12H9c-.98 0-2-1.22-2-2.5 0-.83.42-1.64 1-2.09V6.25c-1.09.53-2 1.84-2 3.25C6 11.31 7.55 13 9 13h4c1.45 0 3-1.69 3-3.5S14.5 6 13 6z"></path></svg></a>行情API</h2>
<ol>
<li>获取交易对/合约列表： GetAssetD</li>
</ol>
<pre><code># 发送请求消息
{"req":"GetAssetD","rid":"0","expires":1537706670830}

# 收到返回消息
{"rid":"0","code":0,
"data":[
{"Sym":"ETH1812","Beg":1,"Expire":1545984000000,"PrzMaxChg":1000,"PrzMinInc":0.05,"PrzMax":1000000,"OrderMaxQty":10000000,"LotSz":1,"PrzM":244.8799999999999954525264911353588104248046875,"MIR":0.07,"MMR":0.05,"OrderMinVal":0,"PrzLatest":244.95,"OpenInterest":2181200,"PrzIndex":244.8863,"PosLmtStart":10000000,"PosOpenRatio":0.05,"FeeMkrR":-0.0003,"FeeTkrR":0.0007,"Mult":1,"FromC":"ETH","ToC":"USD","TrdCls":2,"MkSt":1,"Flag":1,"SettleCoin":"ETH","QuoteCoin":"ETH","SettleR":0.0005,"DenyOpenAfter":1545980400000},
{"Sym":"BTC1812","Beg":1,"Expire":1545984000000,"PrzMaxChg":1000,"PrzMinInc":0.5,"PrzMax":1000000,"OrderMaxQty":10000000,"LotSz":1,"PrzM":6731.3100000000004001776687800884246826171875,"MIR":0.07,"MMR":0.05,"OrderMinVal":0,"PrzLatest":6731.0,"OpenInterest":3431840,"PrzIndex":6737.3525,"PosLmtStart":10000000,"PosOpenRatio":0.05,"FeeMkrR":-0.0003,"FeeTkrR":0.0007,"Mult":1,"FromC":"BTC","ToC":"USD","TrdCls":2,"MkSt":1,"Flag":1,"SettleCoin":"BTC","QuoteCoin":"BTC","SettleR":0.0005,"DenyOpenAfter":1545980400000},
{"Sym":"ETH1809","Beg":1,"Expire":1538121600000,"PrzMaxChg":1000,"PrzMinInc":0.05,"PrzMax":1000000,"OrderMaxQty":10000000,"LotSz":1,"PrzM":244.19999999999998863131622783839702606201171875,"MIR":0.07,"MMR":0.05,"OrderMinVal":0,"PrzLatest":244.20,"OpenInterest":4500733,"PrzIndex":244.8863,"PosLmtStart":10000000,"PosOpenRatio":0.05,"FeeMkrR":-0.0003,"FeeTkrR":0.0007,"Mult":1,"FromC":"ETH","ToC":"USD","TrdCls":2,"MkSt":1,"Flag":1,"SettleCoin":"ETH","QuoteCoin":"ETH","SettleR":0.0005,"DenyOpenAfter":1538118000000},
{"Sym":"BTC1809","Beg":1,"Expire":1538121600000,"PrzMaxChg":1000,"PrzMinInc":0.5,"PrzMax":1000000,"OrderMaxQty":10000000,"LotSz":1,"PrzM":6727.5500000000001818989403545856475830078125,"MIR":0.07,"MMR":0.05,"OrderMinVal":0,"PrzLatest":6728.0,"OpenInterest":1451134,"PrzIndex":6737.3525,"PosLmtStart":10000000,"PosOpenRatio":0.05,"FeeMkrR":-0.0003,"FeeTkrR":0.0007,"Mult":1,"FromC":"BTC","ToC":"USD","TrdCls":2,"MkSt":1,"Flag":1,"SettleCoin":"BTC","QuoteCoin":"BTC","SettleR":0.0005,"DenyOpenAfter":1538118000000}
]}
</code></pre>
<p>用户发送和接收到的所有消息统一采用JSON格式，发送请求的消息参数说明：</p>
<table>
<thead>
<tr>
<th align="left">参数</th>
<th align="left">描述</th>
</tr>
</thead>
<tbody>
<tr>
<td align="left">req</td>
<td align="left">用户的请求操作动作，如： GetAssetD，GetCompositeIndex，GetHistKLine等</td>
</tr>
<tr>
<td align="left">rid</td>
<td align="left">用户发送请求的唯一编号，由于websocket是异步通讯，用户需要通过匹配收到消息的rid和自己发送的rid来匹配操作和应答。</td>
</tr>
<tr>
<td align="left">expires</td>
<td align="left">消息超时，毫秒，建议每次发送请求时填写当前时间加1秒。一般宜在初始化时先用Time消息获取服务端时间,可以相对时差与服务端保持同步。</td>
</tr></tbody></table>
<p>交易对/合约的结构定义如下：</p>
<pre><code>type AssetD struct {
    Sym         string  // 合约符合/交易对符号
    Beg         int64   // 开始时间,毫秒
    DenyOpenAfter   int64   // 到期前禁止开仓时间,毫秒
    Expire      int64   // 到期时间,毫秒
    PrzMaxChg   int32   // 市价委托的撮合的最多次数。比如5
    PrzMinInc   float64 // 最小的价格变化
    PrzMax      float64 // 最大委托价格
    OrderMaxQty float64 // 最大委托数量
    LotSz       float64 // 最小合约数量,当前只支持为1;
    PrzM        float64 // 标记价格
    MIR         float64 // 起始保证金率
    MMR         float64 // 维持保证金率
    OrderMinVal float64 // 委托的最小价值
    PrzLatest   float64 // 最新成交价格
    OpenInterest  int64 // 持仓量
    PrzIndex    float64 // 指数价格
    PosLmtStart int64   // 个人持仓比例激活条件
    PosOpenRatio    float64 // 个人持仓最大比例
    FeeMkrR     float64 // 提供流动性的费率
    FeeTkrR     float64 // 消耗流动性的费率
    Mult        int64   // 乘数
    FromC       string  // 从什么货币
    ToC         string  // 兑换为什么货币
    TrdCls      int32   // 交易类型, 1-现货交易, 2-期货交易, 3-永续
    SettleCoin  string  // 结算货币
    QuoteCoin   string  // 报价货币
    SettleR     float64 // 结算费率
}
</code></pre>
<ol start="2">
<li>获取指数列表： GetCompositeIndex</li>
</ol>
<pre><code># 发送请求消息
{"req":"GetCompositeIndex","rid":"1","expires":1537706670831,"args":{}}

# 收到返回消息
{"rid":"1","code":0,"data":["GMEX_CI_ETH","GMEX_CI_BTC"]}
</code></pre>
<ol start="3">
<li>获取历史K线数据： GetHistKLine</li>
</ol>
<pre><code># 发送请求消息
{"req":"GetHistKLine","rid":"2","expires":1537708009100,"args":{"Sym":"ETH1812","Typ":"1m","beginSec":1537077600,"Offset":0,"Count":10}}

# 收到返回消息
{"rid":"2","code":0,"data":
{"Sym":"ETH1812","Typ":"1m","Count":10,
"Sec":[1537077600,1537077660,1537077720,1537077780,1537077840,1537077900,1537077960,1537078020,1537078080,1537078140],
"PrzOpen":[216.45,216,215.8,215.6,215.05,215.25,215.3,215.45,215.5,215.35],"PrzClose":[216,215.75,215.6,215,215.35,215.45,215.45,215.7,215.4,215.6],
"PrzHigh":[216.5,216,215.9,215.6,215.4,215.5,215.45,215.7,215.55,215.6],"PrzLow":[215.9,215.65,215.6,214.9,215.05,215.2,215.25,215.4,215.35,215.25],
"Volume":[1354,717,473,1751,238,269,94,123,123,275],
"Turnover":[6.26501568815296,3.321813453440903,2.192467668789991,8.137750477124483,1.1054031648919223,1.2493419094774725,0.4364373010900492,0.5702923655266671,0.5709143037474378,1.276362876440699]
}}
</code></pre>
<p>当前系统支持的K线类型有:</p>
<pre><code>(m -&gt; minutes; h -&gt; hours; d -&gt; days; w -&gt; weeks; M -&gt; months):
1m, 3m, 5m, 15m, 30m, 1h, 2h, 4h, 6h, 8h, 12h, 1d, 3d, 1w, 2w, 1M
</code></pre>
<ol start="4">
<li>订阅/取消订阅: Sub / UnSub</li>
</ol>
<pre><code># 发送请求消息
{"req":"Sub","rid":"20","expires":1537708219903,"args":["tick_BTC1812"]}

# 收到返回消息
{"rid":"20","code":0,"data":"OK"}
#
# 收到推送消息
{"subj":"tick","data":{"Sym":"BTC1812","At":1537708218726,"PrzBid1":6723.5,"SzBid1":429,"SzBid":876837,"PrzAsk1":6725.5,"SzAsk1":494,"SzAsk":1022136,"LastPrz":6725,"SettPrz":6723.98,"Prz24":6678.5,"High24":6774,"Low24":6632,"Volume24":5659148,"Turnover24":843.7992005395208,"Volume":40901214,"Turnover":0,"OpenInterest":3436394}}
{"subj":"tick","data":{"Sym":"BTC1812","At":1537708219226,"PrzBid1":6723.5,"SzBid1":429,"SzBid":876837,"PrzAsk1":6725.5,"SzAsk1":494,"SzAsk":1022136,"LastPrz":6724.5,"SettPrz":6723.98,"Prz24":6678.5,"High24":6774,"Low24":6632,"Volume24":5659148,"Turnover24":843.7992005395208,"Volume":40901261,"Turnover":0.006989367239199941,"OpenInterest":3436394}}
.... .... ....
{"subj":"tick","data":{"Sym":"BTC1812","At":1537708237726,"PrzBid1":6723.5,"SzBid1":397,"SzBid":859433,"PrzAsk1":6726.5,"SzAsk1":415,"SzAsk":1010699,"LastPrz":6726,"SettPrz":6725.55,"Prz24":6678.5,"High24":6774,"Low24":6632,"Volume24":5659803,"Turnover24":843.8965933791991,"Volume":40901855,"Turnover":0.0019327980969372585,"OpenInterest":3437022}}

#
# 发送请求消息
{"req":"UnSub","rid":"21","expires":1537708267910,"args":["tick_BTC1812"]}

# 收到返回消息
{"rid":"21","code":0,"data":"OK"}
</code></pre>
<h5><a id="user-content-用户可以订阅的内容有如下" class="anchor" aria-hidden="true" href="#用户可以订阅的内容有如下"><svg class="octicon octicon-link" viewBox="0 0 16 16" version="1.1" width="16" height="16" aria-hidden="true"><path fill-rule="evenodd" d="M4 9h1v1H4c-1.5 0-3-1.69-3-3.5S2.55 3 4 3h4c1.45 0 3 1.69 3 3.5 0 1.41-.91 2.72-2 3.25V8.59c.58-.45 1-1.27 1-2.09C10 5.22 8.98 4 8 4H4c-.98 0-2 1.22-2 2.5S3 9 4 9zm9-3h-1v1h1c1 0 2 1.22 2 2.5S13.98 12 13 12H9c-.98 0-2-1.22-2-2.5 0-.83.42-1.64 1-2.09V6.25c-1.09.53-2 1.84-2 3.25C6 11.31 7.55 13 9 13h4c1.45 0 3-1.69 3-3.5S14.5 6 13 6z"></path></svg></a>用户可以订阅的内容有如下：</h5>
<table>
<thead>
<tr>
<th align="left">订阅内容</th>
<th align="left">描述</th>
</tr>
</thead>
<tbody>
<tr>
<td align="left">TICK</td>
<td align="left">比如： tick_BTC1812</td>
</tr>
<tr>
<td align="left">成交</td>
<td align="left">比如： trade_BTC1812</td>
</tr>
<tr>
<td align="left">20档深度</td>
<td align="left">比如： order20_BTC1812</td>
</tr>
<tr>
<td align="left">全档深度</td>
<td align="left">比如： orderl2_BTC1812</td>
</tr>
<tr>
<td align="left">K线</td>
<td align="left">比如： kline_1m_BTC1812，kline_1h_BTC1812</td>
</tr>
<tr>
<td align="left">指数</td>
<td align="left">比如： index_GMEX_CI_BTC，index_GMEX_CI_ETH</td>
</tr></tbody></table>
<p><strong>NOTE</strong>: UnSub 时可以用 * 一次清空, Sub 时必须提供合法的名字.</p>
<ol start="5">
<li>获取服务器时间</li>
</ol>
<pre><code># 发送请求消息， 由于本消息开销很小，可用于和服务器端保持网络连接用，比如每隔55秒发送一次；
{"req":"Time","rid":"6","expires":1537706745839,"args":1537706744839}
# 收到返回消息
{"rid":"6","code":0,"data":{"time":1537706745295,"data":"1537706744839"}}
</code></pre>
<h2><a id="user-content-交易api" class="anchor" aria-hidden="true" href="#交易api"><svg class="octicon octicon-link" viewBox="0 0 16 16" version="1.1" width="16" height="16" aria-hidden="true"><path fill-rule="evenodd" d="M4 9h1v1H4c-1.5 0-3-1.69-3-3.5S2.55 3 4 3h4c1.45 0 3 1.69 3 3.5 0 1.41-.91 2.72-2 3.25V8.59c.58-.45 1-1.27 1-2.09C10 5.22 8.98 4 8 4H4c-.98 0-2 1.22-2 2.5S3 9 4 9zm9-3h-1v1h1c1 0 2 1.22 2 2.5S13.98 12 13 12H9c-.98 0-2-1.22-2-2.5 0-.83.42-1.64 1-2.09V6.25c-1.09.53-2 1.84-2 3.25C6 11.31 7.55 13 9 13h4c1.45 0 3-1.69 3-3.5S14.5 6 13 6z"></path></svg></a>交易API</h2>
<ol>
<li>用户登录
用户首先要在网站上的个人中心开启API-KEY功能并生成需要的公私秘钥才能使用交易API功能。</li>
</ol>
<pre><code># 发送用户的登录消息
#
# expires 消息超时，毫秒;
# args:
#    UserName 用户注册的登陆名，一般为用户的注册email地址;
#    UserCred 为用户申请的API-KEY;
# signature 用户消息签名: MD5(Req+rid+Args+Expires+API.SecretKey)
# 
# # 例:
# UserName: "example@gaea.com",
# UserCred: "mVAAADjNHzhvehaEvU$BMJoU7BZk"
# SecretKey: "uLgAAHMw62di3hUPypuETMWGzHx852swxM7V0b2HObba5gYNNrLkuvQ4I"
# Req: "Login"
# rid: 1
# 则签名
# signature = md5("Login"+"1"+ JSON.stringify({UserName:"example@gaea.com",UserCred:"mVAAADjNHzhvehaEvU$BMJoU7BZk"}) +"1538222696758" + SecretKey)
# signature结果为:"74c33368e9a1f8d6d13cdf0bf5aa02a8"
# 最终消息体形如
# {"req":"Login","rid":"1","expires":1538222696758,
# "args":{"UserName":"example@gaea.com","UserCred":"mVAAADjNHzhvehaEvU$BMJoU7BZk"},
# "signature": "74c33368e9a1f8d6d13cdf0bf5aa02a8"
# }
#
# 需要注意的是: Args 参数一般为JSON对象(除Time)，在签名时需要序列化为字符串，序列化没有字段顺序要求，但是需要保持签名时序列化的顺序与最终发出消息时序列化的顺序一致。
# 补充: Time消息不要签名

# 收到返回消息
# 注意：
# 这里收到的 UserId 是用户的系统内部唯一编号，简称 UID， 非常重要，系统用此ID后面增加两位数字表示用户的子账户ID，
# 比如UID=1234567，则合约子账号的AId即为123456701；
{"rid":"0","code":0,"data":{"UserName":"gmex-test@gmail.com","UserId":"1234567"}}
</code></pre>
<p>用户成功登录交易系统后，所有用户相关信息会自动推送给用户，如报单变更，仓位变化，成交通知，钱包日志等等。
交易的消息定义和行情类似，但多了签名字段：</p>
<table>
<thead>
<tr>
<th align="left">参数</th>
<th align="left">描述</th>
</tr>
</thead>
<tbody>
<tr>
<td align="left">req</td>
<td align="left">用户的请求操作动作，如： GetAssetD, GetWallets, GetTrades, GetOrders, GetPositions, OrderNew, OrderDel等等。</td>
</tr>
<tr>
<td align="left">rid</td>
<td align="left">用户发送请求的唯一编号，由于websocket是异步通讯，用户需要通过匹配收到消息的rid和自己发送的rid来匹配操作和应答。</td>
</tr>
<tr>
<td align="left">expires</td>
<td align="left">消息超时，毫秒，建议每次发送请求时填写当前时间加1秒。</td>
</tr>
<tr>
<td align="left">args</td>
<td align="left">用户的参数，可选，具体根据req来设置。</td>
</tr>
<tr>
<td align="left">signature</td>
<td align="left">消息签名: MD5(Req+ReqId+Args+Expires+API.SecretKey)，小写。</td>
</tr></tbody></table>
<h1><a id="user-content-重要说明" class="anchor" aria-hidden="true" href="#重要说明"><svg class="octicon octicon-link" viewBox="0 0 16 16" version="1.1" width="16" height="16" aria-hidden="true"><path fill-rule="evenodd" d="M4 9h1v1H4c-1.5 0-3-1.69-3-3.5S2.55 3 4 3h4c1.45 0 3 1.69 3 3.5 0 1.41-.91 2.72-2 3.25V8.59c.58-.45 1-1.27 1-2.09C10 5.22 8.98 4 8 4H4c-.98 0-2 1.22-2 2.5S3 9 4 9zm9-3h-1v1h1c1 0 2 1.22 2 2.5S13.98 12 13 12H9c-.98 0-2-1.22-2-2.5 0-.83.42-1.64 1-2.09V6.25c-1.09.53-2 1.84-2 3.25C6 11.31 7.55 13 9 13h4c1.45 0 3-1.69 3-3.5S14.5 6 13 6z"></path></svg></a>重要说明</h1>
<ul>
<li>UID 和 AID
一个用户对应一个系统内唯一的UID(字符串);
一个用户下面可以有多个AID,AID是UID后面加两位构成;
每个用户注册出来时会自动创建一个默认的AID,就是UID后面加01;
用户可以自己创建多个子账户,因此,一个用户会有多个AID;在系统内部,所有的AID全局唯一;
用户可以在自己的子账户之间相互转移自己的所有的数字货币;
用户下单时指定自己的子账户,该单的风险可以控制在这个子账户范围内,从而可以控制风险.</li>
</ul>
<ol start="2">
<li>查询当前系统的合约列表(必须参数 AId)： GetAssetD</li>
</ol>
<pre><code># 发送请求消息
{"req":"GetAssetD","rid":"2","expires":1537710766358,"args":{"AId":"1525354501"},"signature": "1234567890abcdef1234567890abcdef"}

# 收到返回消息
{"rid":"2","code":0,"data":[
{"Sym":"ETH1812","Beg":1,"Expire":1545984000000,"PrzMaxChg":1000,"PrzMinInc":0.05,"PrzMax":1000000,"OrderMaxQty":10000000,"LotSz":1,"PrzM":245.330000000000012505552149377763271331787109375,"MIR":0.07,"MMR":0.05,"OrderMinVal":0,"PrzLatest":245.35,"OpenInterest":2181137,"PrzIndex":244.9823,"PosLmtStart":10000000,"PosOpenRatio":0.05,"FeeMkrR":-0.0003,"FeeTkrR":0.0007,"Mult":1,"FromC":"ETH","ToC":"USD","TrdCls":2,"MkSt":1,"Flag":1,"SettleCoin":"ETH","QuoteCoin":"ETH","SettleR":0.0005,"DenyOpenAfter":1545980400000},
{"Sym":"BTC1812","Beg":1,"Expire":1545984000000,"PrzMaxChg":1000,"PrzMinInc":0.5,"PrzMax":1000000,"OrderMaxQty":10000000,"LotSz":1,"PrzM":6725.75,"MIR":0.07,"MMR":0.05,"OrderMinVal":0,"PrzLatest":6724.5,"OpenInterest":3431245,"PrzIndex":6729.2552,"PosLmtStart":10000000,"PosOpenRatio":0.05,"FeeMkrR":-0.0003,"FeeTkrR":0.0007,"Mult":1,"FromC":"BTC","ToC":"USD","TrdCls":2,"MkSt":1,"Flag":1,"SettleCoin":"BTC","QuoteCoin":"BTC","SettleR":0.0005,"DenyOpenAfter":1545980400000},
{"Sym":"ETH1809","Beg":1,"Expire":1538121600000,"PrzMaxChg":1000,"PrzMinInc":0.05,"PrzMax":1000000,"OrderMaxQty":10000000,"LotSz":1,"PrzM":244.650000000000005684341886080801486968994140625,"MIR":0.07,"MMR":0.05,"OrderMinVal":0,"PrzLatest":244.65,"OpenInterest":4501232,"PrzIndex":244.9823,"PosLmtStart":10000000,"PosOpenRatio":0.05,"FeeMkrR":-0.0003,"FeeTkrR":0.0007,"Mult":1,"FromC":"ETH","ToC":"USD","TrdCls":2,"MkSt":1,"Flag":1,"SettleCoin":"ETH","QuoteCoin":"ETH","SettleR":0.0005,"DenyOpenAfter":1538118000000},
{"Sym":"BTC1809","Beg":1,"Expire":1538121600000,"PrzMaxChg":1000,"PrzMinInc":0.5,"PrzMax":1000000,"OrderMaxQty":10000000,"LotSz":1,"PrzM":6726.8000000000001818989403545856475830078125,"MIR":0.07,"MMR":0.05,"OrderMinVal":0,"PrzLatest":6724.0,"OpenInterest":1449455,"PrzIndex":6729.2552,"PosLmtStart":10000000,"PosOpenRatio":0.05,"FeeMkrR":-0.0003,"FeeTkrR":0.0007,"Mult":1,"FromC":"BTC","ToC":"USD","TrdCls":2,"MkSt":1,"Flag":1,"SettleCoin":"BTC","QuoteCoin":"BTC","SettleR":0.0005,"DenyOpenAfter":1538118000000}
]}
</code></pre>
<p>返回的结果和行情中获取到的数据是一样的。</p>
<ol start="3">
<li>查询用户子账号的钱包列表信息(必须参数 AId)： GetWallets</li>
</ol>
<pre><code># 发送请求消息
{"req":"GetWallets","rid":"3","expires":1537710967223,"args":{"AId":"1525354501"},"signature": "1234567890abcdef1234567890abcdef"}
# 收到返回消息
{"rid":"3","code":0,"data":[
{"UId":"1234567","AId":"123456701","Coin":"ETH","Depo":1.00000000,"WDrw":0,"PNL":-0.452374713185744188799864398523351731769754619710,"Frz":0,"MI":0.2906262618301145,"RD":0.530702779487773,"Status":2},
{"UId":"1234567","AId":"123456701","Coin":"BTC","Depo":0.16518449,"WDrw":0,"PNL":-0.00426155098522019276893748290683797431047794081854,"Frz":0,"MI":0.04244959641866207,"RD":0.26378834912257804,"Status":2},
{"UId":"1234567","AId":"123456701","Coin":"GAEA","Depo":5,"WDrw":0,"PNL":0,"Frz":0,"Status":2}]}
</code></pre>
<ol start="4">
<li>查询用户子账号的最近的成交记录(必须参数 AId)： GetTrades</li>
</ol>
<pre><code># 发送请求消息
{"req":"GetTrades","rid":"4","expires":1537711037271,"args":{"AId":"123456701"},"signature": "1234567890abcdef1234567890abcdef"}

# 收到返回消息, 默认最多返回200条记录，通过在args中增加设置参数("Start"=0,"Stop"=500)可以最多返回500条记录.
{"rid":"4","code":0,"data":[
{"UId":"1234567","AId":"123456701","Sym":"BTC1812","MatchId":"01CQES0XMV1WWWXP63PMPKE9RF","OrdId":"01CQES0XMV4M3XNJBXKBCKHPZ3","Sz":259,"Prz":6.75E+3,"Fee":-0.00001151111111111111,"FeeCoin":"BTC","At":1537703229547,"Via":7},
{"UId":"1234567","AId":"123456701","Sym":"BTC1812","MatchId":"01CQES0XMVKDQCD192BG4STJF6","OrdId":"01CQES0XMV4M3XNJBXKBCKHPZ3","Sz":519,"Prz":6.75E+3,"Fee":-0.00002306666666666667,"FeeCoin":"BTC","At":1537703229346,"Via":7},
{"UId":"1234567","AId":"123456701","Sym":"BTC1812","MatchId":"01CQES0XMVXD7ZPV22YF49AW93","OrdId":"01CQES0XMV4M3XNJBXKBCKHPZ3","Sz":504,"Prz":6.75E+3,"Fee":-0.00002240000000000000,"FeeCoin":"BTC","At":1537703229146,"Via":7},
{"UId":"1234567","AId":"123456701","Sym":"BTC1812","MatchId":"01CQES0XMV9FDYBTRX29VZ1WAP","OrdId":"01CQES0XMV4M3XNJBXKBCKHPZ3","Sz":585,"Prz":6.75E+3,"Fee":-0.00002600000000000000,"FeeCoin":"BTC","At":1537703228944,"Via":7}
]}
</code></pre>
<ol start="5">
<li>查询用户最长的当前有效的报单列表(必须参数 AId)： GetOrders</li>
</ol>
<pre><code># 发送请求消息
{"req":"GetOrders","rid":"5","expires":1537711298635,"args":{"AId":"123456701"},"signature": "1234567890abcdef1234567890abcdef"}
# 收到返回消息
{"rid":"5","code":0,"data":[
{"UId":"1234567","AId":"123456701","Sym":"ETH1812","OrdId":"01CQES0XMVA1CWXJ823B8TV9FA","COrdId":"1537703785880","Dir":-1,"OType":1,"Prz":265,"Qty":1000,"QtyDsp":0,"PrzStop":0,"At":1537703785905,"Upd":1537703785905,"Until":9223372036854775807,"Frz":0,"Status":2,"QtyF":0,"PrzF":0,"Val":0,"StopPrz":0},
{"UId":"1234567","AId":"123456701","Sym":"BTC1812","OrdId":"01CQES0XMV4FKJMJFFC8SC4EE3","COrdId":"1537703873308","Dir":-1,"OType":1,"Prz":7.10E+3,"Qty":4000,"QtyDsp":0,"PrzStop":0,"At":1537703873327,"Upd":1537703873328,"Until":9223372036854775807,"Frz":0,"Status":2,"QtyF":0,"PrzF":0,"Val":0,"StopPrz":0}
]}
</code></pre>
<ol start="6">
<li>查询用户子账号的当前持仓列表(必须参数 AId)： GetPositions</li>
</ol>
<pre><code># 发送请求消息
{"req":"GetPositions","rid":"6","expires":1537711980986,"args":{"AId":"123456701"},"signature": "1234567890abcdef1234567890abcdef"}

# 收到返回消息
{"rid":"6","code":0,"data":[
{"UId":"1234567","PId":"01CQES0XMVZ2T6GBWVBX62TMMK","AId":"123456701","Sym":"ETH1812","Sz":100,"PrzIni":246.75,"RPNL":0.000121580547112462,"Val":0.4061408496466575,"MMnF":0.020307042482332876,"MI":0.26066975804143216,"UPNL":-0.0008723592717840498,"PrzLiq":154.1425703534361,"PrzBr":146.7046448590807,"FeeEst":0.0002842985947526602,"ROE":-0.04236534514766641,"ADLIdx":-0.07944234516057963},
{"UId":"1234567","PId":"01CQES0XMVJGCXCS0MF1P2ZK5V","AId":"123456701","Sym":"BTC1809","Sz":-120,"PrzIni":6737.5,"RPNL":0.000005343228200371059,"Val":0.017813378143875687,"MMnF":0.0008906689071937843,"UPNL":0.0000026174759721611044,"PrzLiq":63949689.43002453,"PrzBr":67365100.00002584,"FeeEst":0.000012469364700712979,"ROE":0.0028982007003982577,"ADLIdx":0.0007826905463650653,"ADLLight":3},
{"UId":"1234567","PId":"01CQES0XMVS6XK7P4W46ZTY17H","AId":"123456701","Sym":"ETH1809","Sz":50,"PrzIni":245.55,"RPNL":0.00006108735491753208,"Val":0.20377389248889433,"MMnF":0.010188694624444716,"UPNL":-0.00014937609712074688,"PrzLiq":111.97759051807083,"PrzBr":106.57427478640034,"FeeEst":0.000142641724742226,"ROE":-0.014458545542610519,"ADLIdx":-0.027112272106186153}
]}
</code></pre>
<ol start="7">
<li>查询用户子账号的最近已完成的报单列表(必须参数 AId)： GetHistOrders</li>
</ol>
<pre><code># 发送请求消息
{"req":"GetTrades","rid":"7","expires":1537712072667,"args":{"AId":"123456701"},"signature": "1234567890abcdef1234567890abcdef"}

# 收到返回消息
{"rid":"7","code":0,"data":[
{"UId":"1234567","AId":"123456701","Sym":"BTC1809","MatchId":"01CQES0XMV4VFBEETKJ2522BKH","OrdId":"01CQES0XMVW9BFVS1QNZXXE3AC","Sz":-120,"Prz":6737.5,"Fee":-0.000005343228200371059,"FeeCoin":"BTC","At":1537711867713,"Via":7},
{"UId":"1234567","AId":"123456701","Sym":"ETH1809","MatchId":"01CQES0XMVJKBYW747NX6HKYMV","OrdId":"01CQES0XMVM6BR3JQC4Y3TJ0GP","Sz":50,"Prz":245.55,"Fee":-0.00006108735491753208,"FeeCoin":"ETH","At":1537711603240,"Via":7},
{"UId":"1234567","AId":"123456701","Sym":"ETH1812","MatchId":"01CQES0XMVPEVSRHC5N6F44NVQ","OrdId":"01CQES0XMVMQ06C0MDCJ9FN4SC","Sz":100,"Prz":246.75,"Fee":-0.0001215805471124620,"FeeCoin":"ETH","At":1537711597374,"Via":7},
{"UId":"1234567","AId":"123456701","Sym":"BTC1812","MatchId":"01CQES0XMV1WWWXP63PMPKE9RF","OrdId":"01CQES0XMV4M3XNJBXKBCKHPZ3","Sz":259,"Prz":6.75E+3,"Fee":-0.00001151111111111111,"FeeCoin":"BTC","At":1537703229547,"Via":7},
{"UId":"1234567","AId":"123456701","Sym":"BTC1812","MatchId":"01CQES0XMVKDQCD192BG4STJF6","OrdId":"01CQES0XMV4M3XNJBXKBCKHPZ3","Sz":519,"Prz":6.75E+3,"Fee":-0.00002306666666666667,"FeeCoin":"BTC","At":1537703229346,"Via":7},
.... .... .... ....
]}
</code></pre>
<ol start="8">
<li>查询用户子账号的最近钱包日志列表(必须参数 AId)： GetWalletsLog</li>
</ol>
<pre><code># 发送请求消息
{"req":"GetWalletsLog","rid":"9","expires":1537712200855,"args":{"AId":"123456701"},"signature": "1234567890abcdef1234567890abcdef"}

# 收到返回消息
{"rid":"9","code":0,"data":[
{"UId":"1234567","AId":"123456701","Seq":"01CQES0XMVR78FS73QV5WBT3F8","Coin":"BTC","Qty":0.000005343228200371059,"Fee":0,"WalBal":0.1609282822429802,"At":1537711867713,"Op":3,"Via":8,"Info":"Fee :01CQES0XMV4VFBEETKJ2522BKH","Stat":4,"DirtyFlag":11},
{"UId":"1234567","AId":"123456701","Seq":"01CQES0XMVTMQHGPGJZMP9WJKT","Coin":"ETH","Qty":0.00006108735491753208,"Fee":0,"WalBal":0.5478079547162858,"At":1537711603240,"Op":3,"Via":8,"Info":"Fee :01CQES0XMVJKBYW747NX6HKYMV","Stat":4,"DirtyFlag":11},
{"UId":"1234567","AId":"123456701","Seq":"01CQES0XMVYS0W0E1Q69Y9GC3X","Coin":"ETH","Qty":0.0001215805471124620,"Fee":0,"WalBal":0.5477468673613683,"At":1537711597374,"Op":3,"Via":8,"Info":"Fee :01CQES0XMVPEVSRHC5N6F44NVQ","Stat":4,"DirtyFlag":11},
.... .... .... ....
]}
</code></pre>
<ol start="9">
<li>获取服务器时间： Time</li>
</ol>
<pre><code># 发送请求消息， 由于本消息开销很小，可用于和服务器端保持网络连接用，比如每隔55秒发送一次；
{"req":"Time","rid":"13","expires":1537713841078,"args":1537713840076}

# 收到返回消息
{"rid":"13","code":0,"data":{"time":1537713840095,"data":"1537713840076"}}
</code></pre>
<ol start="10">
<li>下单： OrderNew</li>
</ol>
<pre><code># 发送下单请求
# COrdId 是 Client Order ID 的意思，不能为空，由用户生成管理并维护其唯一性，当报单成功后，会对应一个OrdId，为系统能够识别的报单编号;
# 注意，用户发起下单后，要通过 onOrder 消息来监控管理报单的状态变化;
{"req":"OrderNew","rid":"10","expires":1537712923999,
"args":{"AId":"123456701","COrdId":"0","Sym":"BTC1809","Dir":1,"OType":1,"Prz":6500,"Qty":2,"QtyDsp":0,"Tif":0,"OrdFlag":0,"PrzChg":0}
,"signature": "1234567890abcdef1234567890abcdef"}
}

# 收到返回消息
{"rid":"10","code":0,"data":{"UId":"1234567","AId":"123456701","Sym":"BTC1809","OrdId":"01CQES0XMVV3SMWJ7N683FWJR8","COrdId":"0","Dir":1,"OType":1,"Prz":6500,"Qty":2,"QtyDsp":0,"PrzStop":0,"At":1537712923017,"Until":9223372036854775807,"Frz":0,"Status":1,"QtyF":0,"PrzF":0,"Val":0,"StopPrz":0}}
# 后继 onOrder 推送消息会汇报报单的变更情况
{"subj":"onOrder","data":{"WId":"123456701BTC","Prz":6500,"Qty":2,"Upd":1537712923017,"Frz":0,"PrzF":0,"AId":"112562301","COrdId":"0","QtyDsp":0,"PrzStop":0,"Val":0,"Dir":1,"Until":9223372036854776000,"Status":2,"QtyF":0,"StopPrz":0,"UId":"1125623","Sym":"BTC1809","OrdId":"01CQES0XMVV3SMWJ7N683FWJR8","OType":1,"At":1537712923017}}
</code></pre>
<p>报单的参数说明：</p>
<pre><code>args: {
"AId": "账户Id",
"COrdId": "filled by client,客户端自己填写",
"Sym": "BTC1809",  // 交易符号，比如XBTUSD
"Dir": 1,   // 委单方向 买/卖, 1:BID/BUY, -1:ASK/SELL
"OType": 1,  // 报价类型, 1:Limit(限价委单 ), 2: Market(市价委单,匹配后转限价), 3: StopMarket (市价止损);
"Prz": 8000,  // 价格
"Qty": 10000, // 数量(如果&gt;0则为做多,如果&lt;0则为做空)
"QtyDsp": 0,  // 显示数量, 0表示不隐藏, 用于支持冰山委托
"Tif": 0, // 生效时间设定, 0:GoodTillCancel, 1:ImmediateOrCancel/FillAndKill, 2:FillOrKill
"OrdFlag": 0, // 标志位, 0: OF_INVALID, 1: POSTONLY, 2: REDUCEONLY, 4: CLOSEONTRIGGER;
"PrzChg" 0, // 市价成交档位
}
</code></pre>
<ol start="11">
<li>撤单： OrderDel</li>
</ol>
<pre><code># 发送扯单请求
{"req":"OrderDel","rid":"11","expires":1537713298949,"args":{"AId":"123456701","OrdId":"01CQES0XMVV3SMWJ7N683FWJR8","Sym":"BTC1809"}
,"signature": "1234567890abcdef1234567890abcdef"}}

# 收到返回消息
{"rid":"11","code":0,"data":{"UId":"1234567","AId":"123456701","Sym":"BTC1809","OrdId":"01CQES0XMVV3SMWJ7N683FWJR8","Prz":0,"Qty":0,"QtyDsp":0,"PrzStop":0,"At":1537713297967,"Until":9223372036854775807,"Frz":0,"Status":1,"QtyF":0,"PrzF":0,"Val":0,"StopPrz":0}}
# 后继 onOrder 推送消息会汇报报单的变更情况
{"subj":"onOrder","data":{"WId":"123456701BTC","At":1537713297967,"Until":9223372036854776000,"Status":1,"OrdId":"01CQES0XMVV3SMWJ7N683FWJR8","Prz":0,"PrzF":0,"Val":0,"Frz":0,"QtyF":0,"UId":"1234567","Sym":"BTC1809","Qty":0,"PrzStop":0,"AId":"123456701","QtyDsp":0,"Upd":1537713297967,"StopPrz":0}}
{"subj":"onOrder","data":{"Sym":"BTC1809","WId":"123456701BTC","OType":1,"QtyF":0,"PrzF":0,"AId":"123456701","OrdId":"01CQES0XMVV3SMWJ7N683FWJR8","COrdId":"0","Status":5,"StopPrz":0,"Qty":2,"QtyDsp":0,"PrzStop":0,"Val":0,"Frz":0,"UId":"1234567","Dir":1,"Prz":6500,"At":1537712923017,"Upd":1537713297967,"Until":9223372036854776000}
}
</code></pre>
<ol start="12">
<li>设置超时撤单(必须参数 AId)： CancelAllAfter</li>
</ol>
<table>
<thead>
<tr>
<th align="left">参数</th>
<th align="left">描述</th>
</tr>
</thead>
<tbody>
<tr>
<td align="left">AId</td>
<td align="left">用户的子账号ID</td>
</tr>
<tr>
<td align="left">Sec</td>
<td align="left">设置N秒后自动撤销AId下的所有报单</td>
</tr></tbody></table>
<p>调用此接口成功后，用户该AId下的所有报单将在n秒后被全部自动撤单。通过设置0秒可以禁用此功能,常见的使用模式是设 timeout 为 60000，并每隔 15 秒调用一次,建议每次使用完API将Sec设置为0,禁用此功能。</p>
<ol start="13">
<li>用户收到的推送消息</li>
</ol>
<p>用户登录后会收到的推送消息的subj有：
报单通知 onOrder
持仓通知 onPosition
钱包通知 onWallet
钱包日志 onWltLog
成交通知 onTrade</p>
<pre><code>## 比如：钱包变化
{"subj":"onWallet","data":{"PNL":-0.004261550985220193,"MI":0.04240898874506015,"Status":2,"Coin":"BTC","WId":"112562301BTC","Depo":0.16518449,"Frz":0,"RD":0.2635360067663513,"UId":"1234567","AId":"123456701","WDrw":0}}
</code></pre>
<p>对应的数据结构定义如下:</p>
<pre><code>type Ord struct {    // **报单结构体字段定义说明**
    UId     string   // 用户Id
    AId     string   // 账户Id
    Sym     string   // 交易符号，比如BTC1809
    OrdId   string   // 服务器端为其分配的ID
    COrdId  string   // 客户端为其分配的ID
    Dir     int32    // 委单方向 买/卖, 1:BID/BUY, -1:ASK/SELL
    OType   int32    // 报价类型, 1:Limit(限价委单 ), 2: Market(市价委单,匹配后转限价), 3: StopMarket (市价止损);
    Prz     float64  // 报价
    Qty     float64  // 数量(如果&gt;0则为做多,如果&lt;0则为做空)
    QtyDsp  float64  // 显示数量, 0表示不隐藏, 用于支持冰山委托
    Tif     int32    // 生效时间设定, 0:GoodTillCancel, 1:ImmediateOrCancel/FillAndKill, 2:FillOrKill
    OrdFlag int32    // 标志位, 0: OF_INVALID, 1: POSTONLY, 2: REDUCEONLY, 4: CLOSEONTRIGGER;
    Via     int32    // 订单来源
    At      int64    // 报单时间戳，毫秒
    Upd     int64    // 报单更新时间戳，毫秒
    Until   int64    // 有效期，毫秒 。绝对时间
    PrzChg  int32    // 最大价格变动次数， 市价成交档位
    Frz     float64  // 冻结的金额
    ErrCode int32    // 错误编码
    ErrTxt  string   // 错误文本
    Status  int32    // 0-无效, 1-正在排队, 2-有效(撮合中), 3-提交失败, 4-已执行, 5-取消, 6-部分执行, 7-执行失败
    QtyF    float64  // 已成交
    PrzF    float64  // 已成交的平均价格
    Val     float64  // 合约价值:
    StopBy  int32    // 判断依据, 0-PriceMark, 1-PriceLatest, 2-PriceIndex
    StopPrz float64  // 止损价格,止盈价格
    // //////////////////////////////////////////////////////////////////////////////////////////////////
    MM      float64  // 委托保证金 Mgn Initial + 佣金
    FeeEst  float64  // 预估的手续费：按照手续费计算
    UPNLEst float64  // 预估的UPNL	.. Predicatee
}

type Position struct {    // **持仓结构体字段定义说明**
    UId     string   // 用户Id
    AId     string   // 账户Id
    PId     string   // 持仓Id
    Sym     string   // 交易符号，比如BTC1809
    Sz      float64  //仓位(正数为多仓，负数为空仓)
    PrzIni  float64  // 开仓平均价格
    RPNL    float64  // 已实现盈亏
    Val     float64  // 计算值：价值,仓位现时的名义价值，受到标记价格价格的影响
    MMnF    float64  // 保证金，被仓位使用并锁定的保证金
    MI      float64  //
    UPNL    float64  // 计算值：未实现盈亏 PNL==  Profit And Loss
    PrzLiq  float64  // 计算值: 强平价格 亏光当前保证金的 (如果是多仓，并且标记价格低于PrzLiq,则会被强制平仓。/如果是空仓,并缺标记价格高于PrzLiq，则会被强制平仓
    PrzBr   float64  // 计算值: 破产价格 BandRuptcy
    FeeEst  float64  // 预估的平仓费
    // //////////////////////////////////////////////////////////////////////////
    ROE     float64
    ADLIdx  float64  // ADLIdx, 这个是用来排序ADL的
    ADLLight int32   // ADL红绿灯
}

type Wlt struct {    // **钱包结构体字段定义说明**
    UId     string   // 用户Id
    AId     string   // 账户Id
    Coin    string   // 货币符号 BTC/ETH/GAEA
    Depo    float64  // 入金金额
    WDrw    float64  // 出金金额
    PNL     float64  // 已实现盈亏
    Frz     float64  // 冻结金额
    UPNL    float64  // 未实现盈亏：根据持仓情况、标记价格 刷新， 统计值
    MI      float64  // 委托保证金 = 计算自已有委单 + 平仓佣金 + 开仓佣金 Mgn Initial
    MM      float64  // 仓位保证金 + 平仓佣金 Mgn Maintaince
    RD      float64  // 风险度 // Risk Degree.
    Status  int32    // 账户状态，0-INVALID，1-NOT_ACTIVED，2-NORMAL，3-LIQUIDATION，4-TAKEN_OVER
}

type WltLog struct {    // **资金历史结构体字段定义说明**
    UId     string   // 用户Id
    AId     string   // 账户Id
    Seq     string   // 顺序号
    Coin    string   // 货币类型
    Qty     float64  // 货币数量
    Fee     float64  // 手续费
    Peer    string   // 货币地址(假设是出金，则是地址)
    WalBal  float64  //
    At      int64   // 时间
    Op      int32   // 钱包操作: 0-非法, 1-存钱, 2-取钱, 3-已实现盈亏, 4-现货交易, 5-查询
    Via     int32   // OrderVia
    ErrCode int32   // 错误代码
    ErrTxt  string  // 错误文本
    Stat    int32   // OrderStatus
}

type TrdRec struct {    // **成交结构体字段定义说明**
    UId             string  // 用户Id
    AId             string  // 账户Id
    Sym             string  // 交易对符号
    MatchId         string  // 撮合ID
    OrdId           string  // 报单ID
    Sz              float64 // 数量
    Prz             float64 // 价格
    Fee             float64 // 手续费
    FeeCoin         string  // 手续费货币类型
    At              int64   // 成交时间(ms)
    Via             int32   // 报单来源， 0-无效, 1-WEB, 2-APP, 3-API, 4-平仓Liquidate, 5-ADLEngine, 6-Settlement, 7-Trade, 8-Fee, 9-Depo, 10-Wdrw

}

</code></pre>
<h2><a id="user-content-相关术语" class="anchor" aria-hidden="true" href="#相关术语"><svg class="octicon octicon-link" viewBox="0 0 16 16" version="1.1" width="16" height="16" aria-hidden="true"><path fill-rule="evenodd" d="M4 9h1v1H4c-1.5 0-3-1.69-3-3.5S2.55 3 4 3h4c1.45 0 3 1.69 3 3.5 0 1.41-.91 2.72-2 3.25V8.59c.58-.45 1-1.27 1-2.09C10 5.22 8.98 4 8 4H4c-.98 0-2 1.22-2 2.5S3 9 4 9zm9-3h-1v1h1c1 0 2 1.22 2 2.5S13.98 12 13 12H9c-.98 0-2-1.22-2-2.5 0-.83.42-1.64 1-2.09V6.25c-1.09.53-2 1.84-2 3.25C6 11.31 7.55 13 9 13h4c1.45 0 3-1.69 3-3.5S14.5 6 13 6z"></path></svg></a>相关术语</h2>
<table>
<thead>
<tr>
<th align="center">名称</th>
<th align="left">描述</th>
</tr>
</thead>
<tbody>
<tr>
<td align="center">UId</td>
<td align="left">是用户的系统内部唯一编号, 例: UId:123456</td>
</tr>
<tr>
<td align="center">AId</td>
<td align="left">子账户ID, AId是在UId的后面添加两位数字生成, 01为合约ID, 02为现货ID, 例: UId:123456加01则为AId:12345601</td>
</tr>
<tr>
<td align="center">rid</td>
<td align="left">用户发送请求的唯一编号，由于websocket是异步通讯，用户需要通过匹配收到消息的rid和自己发送的rid来匹配操作和应答,值必须为字符串类型</td>
</tr></tbody></table>
<h2><a id="user-content-委托状态码" class="anchor" aria-hidden="true" href="#委托状态码"><svg class="octicon octicon-link" viewBox="0 0 16 16" version="1.1" width="16" height="16" aria-hidden="true"><path fill-rule="evenodd" d="M4 9h1v1H4c-1.5 0-3-1.69-3-3.5S2.55 3 4 3h4c1.45 0 3 1.69 3 3.5 0 1.41-.91 2.72-2 3.25V8.59c.58-.45 1-1.27 1-2.09C10 5.22 8.98 4 8 4H4c-.98 0-2 1.22-2 2.5S3 9 4 9zm9-3h-1v1h1c1 0 2 1.22 2 2.5S13.98 12 13 12H9c-.98 0-2-1.22-2-2.5 0-.83.42-1.64 1-2.09V6.25c-1.09.53-2 1.84-2 3.25C6 11.31 7.55 13 9 13h4c1.45 0 3-1.69 3-3.5S14.5 6 13 6z"></path></svg></a>委托状态码</h2>
<table>
<thead>
<tr>
<th align="center">ErrCode</th>
<th align="left">ErrTxt</th>
<th align="left">描述</th>
</tr>
</thead>
<tbody>
<tr>
<td align="center">0</td>
<td align="left">NORERROR</td>
<td align="left">没有错误</td>
</tr>
<tr>
<td align="center">1</td>
<td align="left">GENERAL</td>
<td align="left">数据错误</td>
</tr>
<tr>
<td align="center">2</td>
<td align="left">DATA</td>
<td align="left">数据错误</td>
</tr>
<tr>
<td align="center">3</td>
<td align="left">NOT_IMPLEMENTED</td>
<td align="left">服务器未实现</td>
</tr>
<tr>
<td align="center">4</td>
<td align="left">NO_MARGIN</td>
<td align="left">保证金不足</td>
</tr>
<tr>
<td align="center">5</td>
<td align="left">FATAL</td>
<td align="left">致命错误</td>
</tr>
<tr>
<td align="center">6</td>
<td align="left">NOT_FOUND</td>
<td align="left">未找到</td>
</tr>
<tr>
<td align="center">7</td>
<td align="left">UNKNOWN_DIR</td>
<td align="left">未知的委托方向</td>
</tr>
<tr>
<td align="center">8</td>
<td align="left">INVALID_CODE</td>
<td align="left">操作码错误</td>
</tr>
<tr>
<td align="center">9</td>
<td align="left">EXISTS</td>
<td align="left">已存在</td>
</tr>
<tr>
<td align="center">10</td>
<td align="left">NOT_FOUND_ORD</td>
<td align="left">没有找到订单号</td>
</tr>
<tr>
<td align="center">11</td>
<td align="left">PRZ_INVALID</td>
<td align="left">价格错误</td>
</tr>
<tr>
<td align="center">12</td>
<td align="left">EXPIRED</td>
<td align="left">已过期</td>
</tr>
<tr>
<td align="center">13</td>
<td align="left">NOT_SUFFICIENT</td>
<td align="left">资金不足</td>
</tr>
<tr>
<td align="center">14</td>
<td align="left">WILLFILL</td>
<td align="left">对于PostOnly，本委托会成交</td>
</tr>
<tr>
<td align="center">15</td>
<td align="left">EXECUTE_FAIL</td>
<td align="left">对FillOrKill委托，这表示执行撮合失败</td>
</tr>
<tr>
<td align="center">16</td>
<td align="left">EXCEED_LIMIT_MINVAL</td>
<td align="left">超过限制</td>
</tr>
<tr>
<td align="center">17</td>
<td align="left">VAL_TOO_SMALL</td>
<td align="left">委托价值太小</td>
</tr>
<tr>
<td align="center">18</td>
<td align="left">EXCEED_LIMIT_PRZ_QTY</td>
<td align="left">价格或者数量超出限制</td>
</tr>
<tr>
<td align="center">19</td>
<td align="left">DENYOPEN_BY_POS</td>
<td align="left">仓位超出限制</td>
</tr>
<tr>
<td align="center">20</td>
<td align="left">DENYOPEN_BY_RD</td>
<td align="left">禁止开仓</td>
</tr>
<tr>
<td align="center">21</td>
<td align="left">TRADE_STOPED</td>
<td align="left">交易暂停</td>
</tr>
<tr>
<td align="center">22</td>
<td align="left">EXCEED_PRZ_LIQ</td>
<td align="left">超过强平价格</td>
</tr>
<tr>
<td align="center">23</td>
<td align="left">TOO_MANY_ORDER</td>
<td align="left">太多的委托</td>
</tr>
<tr>
<td align="center">24</td>
<td align="left">DENYOPEN_BY_TIME</td>
<td align="left">超出开仓时间限制</td>
</tr>
<tr>
<td align="center">25</td>
<td align="left">MD5_INVALID</td>
<td align="left">MD5签名验证错误</td>
</tr>
<tr>
<td align="center">26</td>
<td align="left">RATELIMIT</td>
<td align="left">限速,每秒50次API调用</td>
</tr></tbody></table>
</article>
  </div>

  </div>

  <details class="details-reset details-overlay details-overlay-dark">
    <summary data-hotkey="l" aria-label="Jump to line"></summary>
    <details-dialog class="Box Box--overlay d-flex flex-column anim-fade-in fast linejump" aria-label="Jump to line">
      <!-- '"` --><!-- </textarea></xmp> --></option></form><form class="js-jump-to-line-form Box-body d-flex" action="" accept-charset="UTF-8" method="get"><input name="utf8" type="hidden" value="&#x2713;" />
        <input class="form-control flex-auto mr-3 linejump-input js-jump-to-line-field" type="text" placeholder="Jump to line&hellip;" aria-label="Jump to line" autofocus>
        <button type="submit" class="btn" data-close-dialog>Go</button>
</form>    </details-dialog>
  </details>


  </div>
  <div class="modal-backdrop js-touch-events"></div>
</div>

    </div>
  </div>

  </div>

        
<div class="footer container-lg px-3" role="contentinfo">
  <div class="position-relative d-flex flex-justify-between pt-6 pb-2 mt-6 f6 text-gray border-top border-gray-light ">
    <ul class="list-style-none d-flex flex-wrap ">
      <li class="mr-3">&copy; 2018 <span title="0.22497s from unicorn-5d5c59d4b5-qcm9h">GitHub</span>, Inc.</li>
        <li class="mr-3"><a data-ga-click="Footer, go to terms, text:terms" href="https://github.com/site/terms">Terms</a></li>
        <li class="mr-3"><a data-ga-click="Footer, go to privacy, text:privacy" href="https://github.com/site/privacy">Privacy</a></li>
        <li class="mr-3"><a href="https://help.github.com/articles/github-security/" data-ga-click="Footer, go to security, text:security">Security</a></li>
        <li class="mr-3"><a href="https://status.github.com/" data-ga-click="Footer, go to status, text:status">Status</a></li>
        <li><a data-ga-click="Footer, go to help, text:help" href="https://help.github.com">Help</a></li>
    </ul>

    <a aria-label="Homepage" title="GitHub" class="footer-octicon mr-lg-4" href="https://github.com">
      <svg height="24" class="octicon octicon-mark-github" viewBox="0 0 16 16" version="1.1" width="24" aria-hidden="true"><path fill-rule="evenodd" d="M8 0C3.58 0 0 3.58 0 8c0 3.54 2.29 6.53 5.47 7.59.4.07.55-.17.55-.38 0-.19-.01-.82-.01-1.49-2.01.37-2.53-.49-2.69-.94-.09-.23-.48-.94-.82-1.13-.28-.15-.68-.52-.01-.53.63-.01 1.08.58 1.23.82.72 1.21 1.87.87 2.33.66.07-.52.28-.87.51-1.07-1.78-.2-3.64-.89-3.64-3.95 0-.87.31-1.59.82-2.15-.08-.2-.36-1.02.08-2.12 0 0 .67-.21 2.2.82.64-.18 1.32-.27 2-.27.68 0 1.36.09 2 .27 1.53-1.04 2.2-.82 2.2-.82.44 1.1.16 1.92.08 2.12.51.56.82 1.27.82 2.15 0 3.07-1.87 3.75-3.65 3.95.29.25.54.73.54 1.48 0 1.07-.01 1.93-.01 2.2 0 .21.15.46.55.38A8.013 8.013 0 0 0 16 8c0-4.42-3.58-8-8-8z"/></svg>
</a>
   <ul class="list-style-none d-flex flex-wrap ">
        <li class="mr-3"><a data-ga-click="Footer, go to contact, text:contact" href="https://github.com/contact">Contact GitHub</a></li>
        <li class="mr-3"><a href="https://github.com/pricing" data-ga-click="Footer, go to Pricing, text:Pricing">Pricing</a></li>
      <li class="mr-3"><a href="https://developer.github.com" data-ga-click="Footer, go to api, text:api">API</a></li>
      <li class="mr-3"><a href="https://training.github.com" data-ga-click="Footer, go to training, text:training">Training</a></li>
        <li class="mr-3"><a href="https://blog.github.com" data-ga-click="Footer, go to blog, text:blog">Blog</a></li>
        <li><a data-ga-click="Footer, go to about, text:about" href="https://github.com/about">About</a></li>

    </ul>
  </div>
  <div class="d-flex flex-justify-center pb-6">
    <span class="f6 text-gray-light"></span>
  </div>
</div>



  <div id="ajax-error-message" class="ajax-error-message flash flash-error">
    <svg class="octicon octicon-alert" viewBox="0 0 16 16" version="1.1" width="16" height="16" aria-hidden="true"><path fill-rule="evenodd" d="M8.893 1.5c-.183-.31-.52-.5-.887-.5s-.703.19-.886.5L.138 13.499a.98.98 0 0 0 0 1.001c.193.31.53.501.886.501h13.964c.367 0 .704-.19.877-.5a1.03 1.03 0 0 0 .01-1.002L8.893 1.5zm.133 11.497H6.987v-2.003h2.039v2.003zm0-3.004H6.987V5.987h2.039v4.006z"/></svg>
    <button type="button" class="flash-close js-ajax-error-dismiss" aria-label="Dismiss error">
      <svg class="octicon octicon-x" viewBox="0 0 12 16" version="1.1" width="12" height="16" aria-hidden="true"><path fill-rule="evenodd" d="M7.48 8l3.75 3.75-1.48 1.48L6 9.48l-3.75 3.75-1.48-1.48L4.52 8 .77 4.25l1.48-1.48L6 6.52l3.75-3.75 1.48 1.48L7.48 8z"/></svg>
    </button>
    You can’t perform that action at this time.
  </div>


    
    <script crossorigin="anonymous" integrity="sha512-j7P2Pw3104HznNqyNm7WuCF8Lstcf/sPX5meP6e5RFF177kmi6SAbkZ52A3ttKj0cRHLRrUbk7C1w1xtwh52zA==" type="application/javascript" src="https://assets-cdn.github.com/assets/frameworks-c163002918ede72971a36e0025f67a4a.js"></script>
    
    <script crossorigin="anonymous" async="async" integrity="sha512-gtafyIv6SUhe0hVSBIE4wGG2amzWBhSqbqnU0IhJ6jRFWJTPtR4YM6aCEg04g8Xybb1tp12e9aDxig9cbu6e6g==" type="application/javascript" src="https://assets-cdn.github.com/assets/github-8d674aa76ee19b76d61e8afe7d9b1209.js"></script>
    
    
    
  <div class="js-stale-session-flash stale-session-flash flash flash-warn flash-banner d-none">
    <svg class="octicon octicon-alert" viewBox="0 0 16 16" version="1.1" width="16" height="16" aria-hidden="true"><path fill-rule="evenodd" d="M8.893 1.5c-.183-.31-.52-.5-.887-.5s-.703.19-.886.5L.138 13.499a.98.98 0 0 0 0 1.001c.193.31.53.501.886.501h13.964c.367 0 .704-.19.877-.5a1.03 1.03 0 0 0 .01-1.002L8.893 1.5zm.133 11.497H6.987v-2.003h2.039v2.003zm0-3.004H6.987V5.987h2.039v4.006z"/></svg>
    <span class="signed-in-tab-flash">You signed in with another tab or window. <a href="">Reload</a> to refresh your session.</span>
    <span class="signed-out-tab-flash">You signed out in another tab or window. <a href="">Reload</a> to refresh your session.</span>
  </div>
  <div class="facebox" id="facebox" style="display:none;">
  <div class="facebox-popup">
    <div class="facebox-content" role="dialog" aria-labelledby="facebox-header" aria-describedby="facebox-description">
    </div>
    <button type="button" class="facebox-close js-facebox-close" aria-label="Close modal">
      <svg class="octicon octicon-x" viewBox="0 0 12 16" version="1.1" width="12" height="16" aria-hidden="true"><path fill-rule="evenodd" d="M7.48 8l3.75 3.75-1.48 1.48L6 9.48l-3.75 3.75-1.48-1.48L4.52 8 .77 4.25l1.48-1.48L6 6.52l3.75-3.75 1.48 1.48L7.48 8z"/></svg>
    </button>
  </div>
</div>

  <template id="site-details-dialog">
  <details class="details-reset details-overlay details-overlay-dark lh-default text-gray-dark" open>
    <summary aria-haspopup="dialog" aria-label="Close dialog"></summary>
    <details-dialog class="Box Box--overlay d-flex flex-column anim-fade-in fast">
      <button class="Box-btn-octicon m-0 btn-octicon position-absolute right-0 top-0" type="button" aria-label="Close dialog" data-close-dialog>
        <svg class="octicon octicon-x" viewBox="0 0 12 16" version="1.1" width="12" height="16" aria-hidden="true"><path fill-rule="evenodd" d="M7.48 8l3.75 3.75-1.48 1.48L6 9.48l-3.75 3.75-1.48-1.48L4.52 8 .77 4.25l1.48-1.48L6 6.52l3.75-3.75 1.48 1.48L7.48 8z"/></svg>
      </button>
      <div class="octocat-spinner my-6 js-details-dialog-spinner"></div>
    </details-dialog>
  </details>
</template>

  <div class="Popover js-hovercard-content position-absolute" style="display: none; outline: none;" tabindex="0">
  <div class="Popover-message Popover-message--bottom-left Popover-message--large Box box-shadow-large" style="width:360px;">
  </div>
</div>

<div id="hovercard-aria-description" class="sr-only">
  Press h to open a hovercard with more details.
</div>


  </body>
</html>

