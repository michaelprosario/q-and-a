module.exports = ({ env }) => ({
  auth: {
    secret: env('ADMIN_JWT_SECRET', '87c4570bc3f7cf4493a872d2841d851f'),
  },
});
